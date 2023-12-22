using Common;
using Common.Constants;
using Common.Pagging;
using Common.Shared;
using Dapper;
using Entity.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Repository;
using Serilog;
using Services.Interfaces;
using Services.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utility;
using ViewModel.ListBoxModel;
using ViewModel.RequestModel;
using ViewModel.ViewModels;

namespace Services.Implement
{
    public class RulesServices : IRulesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly IUploadFileServices _uploadFileServices;
        private readonly IModelUtility _modelUtility;
        private readonly AppSettings _appSettings;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public RulesServices(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, IUploadFileServices uploadFileServices,
            IModelUtility modelUtility, IOptions<AppSettings> options, IAuthenticatedUserService authenticatedUser)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _uploadFileServices = uploadFileServices;
            _modelUtility = modelUtility;
            _appSettings = options.Value;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<IPagedResult<bool>> DeleteAsync(long id)
        {
            if (id == 0)
            {
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                    ResponseMessage = Constants.ErrorMessageCodes.IdNotFound,
                    Data = false
                };
            }
            try
            {
                await _unitOfWork.OpenTransaction();
                var rule = await _unitOfWork.RulesRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                if (rule != null && rule.Id > 0)
                {
                    var p = new DynamicParameters();
                    p.Add("@Id", rule.Id);
                    p.Add("@Title", rule.Title);
                    p.Add("@TableName", "Rules");
                    var checkExists = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("spCheckExistsWhenDelete", p);
                    if (checkExists?.FirstOrDefault()?.Value != string.Empty)
                    {
                        return new PagedResult<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                            ResponseMessage = checkExists?.FirstOrDefault()?.Value,
                            Data = false
                        };
                    }
                    rule.IsDeleted = true;
                    _unitOfWork.RulesRepository.Update(rule);
                    await _unitOfWork.RulesRepository.CommitAsync().ConfigureAwait(false);
                    _unitOfWork.CommitTransaction();
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.DeleteSuccess,
                        Data = true
                    };
                }
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                    ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    Data = false
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " RulesServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResults<RuleViewModel>> GetAllAsync(PagingRequest request)
        {
            try
            {
                request ??= new PagingRequest
                {
                    Page = 1,
                    PageSize = 10,
                    SortDir = Constants.SortDesc,
                    SortField = Constants.SortId
                };
                var p = new DynamicParameters();
                p.Add(Constants.Key, request.SearchText);
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var listRule = await _dapperRepository.QueryMultipleUsingStoreProcAsync<RuleViewModel>("uspRules_selectAll", p);
                var ruleEnumerable = listRule.ToList();

                var total = ruleEnumerable.Count;
                listRule = !string.IsNullOrEmpty(request.SortField) ? ruleEnumerable.OrderBy(request) : ruleEnumerable.OrderByDescending(x => x.Id);

                if (request.Page != null && request.PageSize.HasValue)
                {
                    listRule = listRule.Paging(request);
                }

                if (null == listRule)
                {
                    return new PagedResults<RuleViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var viewModels = listRule.ToList();
                viewModels.ForEach(x =>
                {
                    var listAttachmentReName = new List<string>();
                    if (!string.IsNullOrEmpty(x.AttachmentFileReName))
                    {
                        var list = x.AttachmentFileReName.Split(Constants.Semicolon);
                        listAttachmentReName.AddRange(list.Select(attachmentFileReName => _appSettings.DomainFile + attachmentFileReName));
                    }
                    x.ListAttachmentReName = listAttachmentReName;

                    var listAttachmentOriginalName = new List<string>();
                    if (!string.IsNullOrEmpty(x.AttachmentFileOriginalName))
                    {
                        var list = x.AttachmentFileOriginalName.Split(Constants.Semicolon);
                        listAttachmentOriginalName.AddRange(list.Select(attachmentFileOriginalName => attachmentFileOriginalName));
                    }
                    x.ListAttachmentOriginalName = listAttachmentOriginalName;
                });
                viewModels.ForEach(x =>
                {
                    var huylich = JsonConvert.DeserializeObject<CancellationOfSchedulePenalty>(x.CancellationOfSchedulePenalty);

                    x.HangSchedule = huylich.HangSchedule;
                    x.QuitWorkingPernalty = huylich.QuitWorkingPernalty;
                    try
                    {
                        var nono = JsonConvert.DeserializeObject<List<RuleRewardAndPunish>>(huylich.NoReplacePenalty);
                        if (nono != null && nono.Count() > 0)
                        {
                            var ketqua = new List<string>();
                            foreach (var item in nono)
                            {
                                ketqua.Add(item.Unit + "d" + "=" + item.Price + "k");
                            }
                            x.NoReplacePenalty = string.Join(";", ketqua);
                        }

                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        var nono = JsonConvert.DeserializeObject<List<RuleRewardAndPunish>>(x.MoldBonus);
                        if (nono != null && nono.Count() > 0)
                        {
                            var ketqua = new List<string>();
                            foreach (var item in nono)
                            {
                                ketqua.Add(item.Unit + "xe" + "=" + item.Price + "k");
                            }
                            x.MoldBonus = string.Join(";", ketqua);
                        }

                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        var nono = JsonConvert.DeserializeObject<List<RuleRewardAndPunish>>(x.SignUpBonus);
                        if (nono != null && nono.Count() > 0)
                        {
                            var ketqua = new List<string>();
                            foreach (var item in nono)
                            {
                                ketqua.Add(item.Unit + "d" + "=" + item.Price + "k");
                            }
                            x.SignUpBonus = string.Join(";", ketqua);
                        }

                    }
                    catch (Exception)
                    {
                    }
                });

                return new PagedResults<RuleViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = viewModels,
                    TotalRecords = total
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " RulesServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResults<ItemModel>> SuggestionAsync(string searchText)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add(Constants.Key, searchText);
                p.Add(Constants.CompanyId, _authenticatedUser.CompanyId);
                var lstRules = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ItemModel>("uspRules_Suggestion", p);

                if (null == lstRules)
                {
                    return new PagedResults<ItemModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.ErrorMessageCodes.RecordNotFoundMessage,
                    };
                }

                var itemModels = lstRules.ToList();
                return new PagedResults<ItemModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SuccessMessage,
                    ListData = itemModels,
                    TotalRecords = itemModels.Count
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " RulesServices: " + ex.Message);
                throw;
            }
        }

        public async Task<IPagedResult<bool>> SaveAsync(RuleRequest model, List<IFormFile> files)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var rule = ChangeDataModel(model);
                rule.AttachmentFileReName = _uploadFileServices.GetFileNameMultipleFileReNameAsync(files);
                if (rule.Id > 0)
                {
                    var fileNameOld = _unitOfWork.RulesRepository.GetSingle(x => x.Id == rule.Id).AttachmentFileReName ?? string.Empty;
                    var update = _modelUtility.ObjectUpdateToPrams(rule);
                    var result = await _unitOfWork.RulesRepository.UpdateUsingDapper(rule, "uspRules_Update", update);
                    if (result)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, rule.AttachmentFileReName, fileNameOld);
                    }
                }
                else
                {
                    var insert = _modelUtility.ObjectCreateToPrams(rule);
                    var result = await _unitOfWork.RulesRepository.InsertUsingDapper("uspRules_Insert", insert);
                    if (result)
                    {
                        _uploadFileServices.UploadMultipleFileAsync(files, rule.AttachmentFileReName, string.Empty);
                    }
                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " RulesServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<IPagedResult<Rules>> GetDetailAsync(long id)
        {
            try
            {
                await _unitOfWork.OpenTransaction();
                var rule = await _unitOfWork.RulesRepository.GetSingleAsync(x => x.Id == id);
                _unitOfWork.CommitTransaction();
                return new PagedResult<Rules>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.SaveSuccess,
                    Data = rule
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " RulesServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }


        private Rules ChangeDataModel(RuleRequest model)
        {
            var items = model.NoReplacePenalty.Split(";");
            var signUpBonus = model.SignUpBonus.Split(";");
            var mold = model.MoldBonus.Split(";");
            var noReplacePenaltys = new List<RuleRewardAndPunish>();
            if (items.Length > 0)
            {
                try
                {
                    foreach (var item in items)
                    {
                        var slitUnit = item.Split("=");
                        if (slitUnit.Length > 0)
                        {
                            var first = int.Parse(slitUnit[0].Substring(0, slitUnit[0].Length - 1));
                            var last = float.Parse(slitUnit[1].Substring(0, slitUnit[1].Length - 1));
                            noReplacePenaltys.Add(new RuleRewardAndPunish
                            {
                                Price = last,
                                Unit = first
                            });
                        }
                    }
                    model.NoReplacePenalty = JsonConvert.SerializeObject(noReplacePenaltys);
                    noReplacePenaltys = new List<RuleRewardAndPunish>();
                }
                catch (Exception)
                {
                }

            }
            if (signUpBonus.Length > 0)
            {
                try
                {
                    foreach (var item in signUpBonus)
                    {
                        var slitUnit = item.Split("=");
                        if (slitUnit.Length > 0)
                        {
                            var first = int.Parse(slitUnit[0].Substring(0, slitUnit[0].Length - 1));
                            var last = float.Parse(slitUnit[1].Substring(0, slitUnit[1].Length - 1));
                            noReplacePenaltys.Add(new RuleRewardAndPunish
                            {
                                Price = last,
                                Unit = first
                            });
                        }
                    }
                    model.SignUpBonus = JsonConvert.SerializeObject(noReplacePenaltys);
                    noReplacePenaltys = new List<RuleRewardAndPunish>();
                }
                catch (Exception)
                {
                }
            }
            if (mold.Length > 0)
            {
                try
                {
                    foreach (var item in mold)
                    {
                        var slitUnit = item.Split("=");
                        if (slitUnit.Length > 0)
                        {
                            var first = int.Parse(slitUnit[0].Substring(0, slitUnit[0].Length - 2));
                            var last = float.Parse(slitUnit[1].Substring(0, slitUnit[1].Length - 1));
                            noReplacePenaltys.Add(new RuleRewardAndPunish
                            {
                                Price = last,
                                Unit = first
                            });
                        }
                    }
                    model.SignUpBonus = JsonConvert.SerializeObject(noReplacePenaltys);
                    noReplacePenaltys = new List<RuleRewardAndPunish>();
                }
                catch (Exception)
                {
                }

            }
            var cancelPenalty = new CancellationOfSchedulePenalty(model.HangSchedule, model.NoReplacePenalty, model.QuitWorkingPernalty);
            return new Rules
            {
                Id = model.Id,
                Day = model.Day,
                PlaceId = model.PlaceId,
                LaborWages = model.LaborWages,
                SalarySupervisor = model.SalarySupervisor,
                MinimumQuantity = model.MinimumQuantity,
                VehicleSizeFactorA = model.VehicleSizeFactorA,
                VehicleSizeFactorB = model.VehicleSizeFactorB,
                VehicleSizeFactorC = model.VehicleSizeFactorC,
                VehicleSizeFactorD = model.VehicleSizeFactorD,
                VehicleSizeFactorE = model.VehicleSizeFactorE,
                VehicleSizeFactorF = model.VehicleSizeFactorF,
                VehicleSizeFactorM = model.VehicleSizeFactorM,
                VehicleSizeFactorS = model.VehicleSizeFactorS,
                WeatherCoefficient = model.WeatherCoefficient,
                ContingencyCoefficient = model.ContingencyCoefficient,
                PileRegistration = model.PileRegistration,
                DayPayroll = model.DayPayroll,
                SignUpBonus = model.SignUpBonus,
                MoldBonus = model.MoldBonus,
                CancellationOfSchedulePenalty = JsonConvert.SerializeObject(cancelPenalty),
                Title = model.Title,
                Subtitle = model.Subtitle,
                Description = model.Description,
                History = model.History,
            };
        }
    }
}
