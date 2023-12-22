using AutoMapper;
using Common;
using Common.Constants;
using Common.Pagging;
using Entity.Model;
using Microsoft.AspNetCore.Hosting;
using Repository;
using Serilog;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Utility;
using ViewModel.RequestModel.Upload;
using ViewModel.Requests;

namespace Services.Implement
{
    public class ImportExcelServices : IImportExcelServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static string _webRootPath = string.Empty;

        public ImportExcelServices(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webRootPath = hostingEnvironment.WebRootPath + Constants.Excel;
        }

        public async Task<IPagedResult<bool>> UploadAsync(FileModel model)
        {
            try
            {
                //Create - Delete - Update
                await _unitOfWork.OpenTransaction();

                var getExtension = Path.GetExtension(ContentDispositionHeaderValue.Parse(model.File.ContentDisposition).FileName.Trim('"'));

                if (getExtension != ".xls" && getExtension != ".xlsx" && getExtension != ".csv")
                {
                    _unitOfWork.CommitTransaction();
                    return new PagedResult<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.ErrorMessageCodes.FileCorrectFormat,
                        Data = false
                    };
                }

                if (!Directory.Exists(_webRootPath))
                {
                    Directory.CreateDirectory(_webRootPath);
                }

                var fileName = model.File.FileName.Replace(getExtension, string.Empty) + Constants.Underlined + MurmurHash.Hash(model.File.FileName + Guid.NewGuid()) + getExtension;
                var filePath = Path.Combine(_webRootPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                }
                DataTable convertExcelToDataTable;
                if (getExtension == ".xls" || getExtension == ".xlsx")
                {
                    convertExcelToDataTable = DataTableUtility.ConvertExeLtoDataTable(filePath);
                }
                else
                {
                    convertExcelToDataTable = DataTableUtility.ConvertCsVtoDataTable(filePath);
                }

                switch (model.TableName)
                {
                    case "B2B":
                        List<B2BUpload> b2B;
                        b2B = DataTableUtility.ConvertDataTable<B2BUpload>(convertExcelToDataTable);
                        UploadB2B(b2B);
                        break;
                    case "Basements":
                        List<BasementUpload> basement;
                        basement = DataTableUtility.ConvertDataTable<BasementUpload>(convertExcelToDataTable);
                        UploadBasements(basement);
                        break;
                    case "Brands":
                        List<BrandUpload> brand;
                        brand = DataTableUtility.ConvertDataTable<BrandUpload>(convertExcelToDataTable);
                        UploadBrands(brand);
                        break;
                    case "CarModels":
                        List<CarModelUpload> carModel;
                        carModel = DataTableUtility.ConvertDataTable<CarModelUpload>(convertExcelToDataTable);
                        UploadCarModels(carModel);
                        break;
                    case "Cars":
                        List<CarUpload> car;
                        car = DataTableUtility.ConvertDataTable<CarUpload>(convertExcelToDataTable);
                        UploadCars(car);
                        break;
                    case "Class":
                        List<ClassUpload> classs;
                        classs = DataTableUtility.ConvertDataTable<ClassUpload>(convertExcelToDataTable);
                        UploadClass(classs);
                        break;
                    case "ColorCode":
                        List<ColorCodeUpload> colorCode;
                        colorCode = DataTableUtility.ConvertDataTable<ColorCodeUpload>(convertExcelToDataTable);
                        UploadColorCode(colorCode);
                        break;
                    case "Columns":
                        List<ColumnUpload> column;
                        column = DataTableUtility.ConvertDataTable<ColumnUpload>(convertExcelToDataTable);
                        UploadColumns(column);
                        break;
                    case "Company":
                        List<CompanyUpload> company;
                        company = DataTableUtility.ConvertDataTable<CompanyUpload>(convertExcelToDataTable);
                        UploadCompany(company);
                        break;
                    case "Customers":
                        List<CustomerUpload> customer;
                        customer = DataTableUtility.ConvertDataTable<CustomerUpload>(convertExcelToDataTable);
                        UploadCustomers(customer);
                        break;
                    case "DataType":
                        List<DataTypeUpload> dataType;
                        dataType = DataTableUtility.ConvertDataTable<DataTypeUpload>(convertExcelToDataTable);
                        UploadDataType(dataType);
                        break;
                    case "Jobs":
                        List<JobUpload> job;
                        job = DataTableUtility.ConvertDataTable<JobUpload>(convertExcelToDataTable);
                        UploadJobs(job);
                        break;
                    case "Places":
                        List<PlaceUpload> places;
                        places = DataTableUtility.ConvertDataTable<PlaceUpload>(convertExcelToDataTable);
                        UploadPlaces(places);
                        break;
                    case "Prices":
                        List<PriceUpload> prices;
                        prices = DataTableUtility.ConvertDataTable<PriceUpload>(convertExcelToDataTable);
                        UploadPrices(prices);
                        break;
                    case "Rules":
                        List<RuleUpload> rules;
                        rules = DataTableUtility.ConvertDataTable<RuleUpload>(convertExcelToDataTable);
                        UploadRules(rules);
                        break;
                    case "Slots":
                        List<SlotUpload> slots;
                        slots = DataTableUtility.ConvertDataTable<SlotUpload>(convertExcelToDataTable);
                        UploadSlots(slots);
                        break;
                    case "Staffs":
                        List<StaffUpload> staffs;
                        staffs = DataTableUtility.ConvertDataTable<StaffUpload>(convertExcelToDataTable);
                        UploadStaffs(staffs);
                        break;
                    case "StaffPlace":
                        List<StaffPlaceUpload> staffPlace;
                        staffPlace = DataTableUtility.ConvertDataTable<StaffPlaceUpload>(convertExcelToDataTable);
                        UploadStaffPlace(staffPlace);
                        break;
                    case "Subscriptions":
                        List<SubscriptionUpload> subscriptions;
                        subscriptions = DataTableUtility.ConvertDataTable<SubscriptionUpload>(convertExcelToDataTable);
                        UploadSubscriptions(subscriptions);
                        break;
                    case "TeamLead":
                        List<TeamLeadUpload> teamLead;
                        teamLead = DataTableUtility.ConvertDataTable<TeamLeadUpload>(convertExcelToDataTable);
                        UploadTeamLead(teamLead);
                        break;
                    case "TeamPlaces":
                        List<TeamPlaceUpload> teamPlaces;
                        teamPlaces = DataTableUtility.ConvertDataTable<TeamPlaceUpload>(convertExcelToDataTable);
                        UploadTeamPlaces(teamPlaces);
                        break;
                    case "Teams":
                        List<TeamUpload> teams;
                        teams = DataTableUtility.ConvertDataTable<TeamUpload>(convertExcelToDataTable);
                        UploadTeams(teams);
                        break;
                    case "TeamWorker":
                        List<TeamWorkerUpload> teamWorker;
                        teamWorker = DataTableUtility.ConvertDataTable<TeamWorkerUpload>(convertExcelToDataTable);
                        UploadTeamWorker(teamWorker);
                        break;
                    case "TeamZone":
                        List<TeamZoneUpload> teamZone;
                        teamZone = DataTableUtility.ConvertDataTable<TeamZoneUpload>(convertExcelToDataTable);
                        UploadTeamZone(teamZone);
                        break;
                    case "Withdraws":
                        List<WithdrawUpload> withdraws;
                        withdraws = DataTableUtility.ConvertDataTable<WithdrawUpload>(convertExcelToDataTable);
                        UploadWithdraws(withdraws);
                        break;
                    case "WorkerPlace":
                        List<WorkerPlaceUpload> workerPlace;
                        workerPlace = DataTableUtility.ConvertDataTable<WorkerPlaceUpload>(convertExcelToDataTable);
                        UploadWorkerPlace(workerPlace);
                        break;
                    case "Workers":
                        List<WorkerUpload> workers;
                        workers = DataTableUtility.ConvertDataTable<WorkerUpload>(convertExcelToDataTable);
                        UploadWorkers(workers);
                        break;
                    case "ZoneColumn":
                        List<ZoneColumnUpload> zoneColumn;
                        zoneColumn = DataTableUtility.ConvertDataTable<ZoneColumnUpload>(convertExcelToDataTable);
                        UploadZoneColumn(zoneColumn);
                        break;
                    case "Zones":
                        List<ZoneUpload> zones;
                        zones = DataTableUtility.ConvertDataTable<ZoneUpload>(convertExcelToDataTable);
                        UploadZones(zones);
                        break;
                    case "UserProfile":
                        List<AppUserUpload> userProfile;
                        userProfile = DataTableUtility.ConvertDataTable<AppUserUpload>(convertExcelToDataTable);
                        UploadUserProfile(userProfile);
                        break;
                }

                if (File.Exists(filePath))
                {
                    //File.Create(filePath).Close();
                    File.Delete(filePath);
                    Log.Information("Delete File: ", fileName);

                }
                _unitOfWork.CommitTransaction();
                return new PagedResult<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.ErrorMessageCodes.UploadSuccess,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ImportExcelServices: " + ex.Message);
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        private void UploadB2B(List<B2BUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<B2B>>(insert);
                _unitOfWork.B2BRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<B2B>>(update);
                _unitOfWork.B2BRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<B2B>>(delete);
                _unitOfWork.B2BRepository.BulkDelete(listDelete);
            }
        }

        private void UploadBasements(List<BasementUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Basements>>(insert);
                _unitOfWork.BasementsRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Basements>>(update);
                _unitOfWork.BasementsRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Basements>>(delete);
                _unitOfWork.BasementsRepository.BulkDelete(listDelete);
            }
        }

        private void UploadBrands(List<BrandUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Brands>>(insert);
                _unitOfWork.BrandsRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Brands>>(update);
                _unitOfWork.BrandsRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Brands>>(delete);
                _unitOfWork.BrandsRepository.BulkDelete(listDelete);
            }
        }

        private void UploadCarModels(List<CarModelUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<CarModels>>(insert);
                _unitOfWork.CarModelsRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<CarModels>>(update);
                _unitOfWork.CarModelsRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<CarModels>>(delete);
                _unitOfWork.CarModelsRepository.BulkDelete(listDelete);
            }
        }

        private void UploadCars(List<CarUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Cars>>(insert);
                _unitOfWork.CarsRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Cars>>(update);
                _unitOfWork.CarsRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Cars>>(delete);
                _unitOfWork.CarsRepository.BulkDelete(listDelete);
            }
        }

        private void UploadClass(List<ClassUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Class>>(insert);
                _unitOfWork.ClassRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Class>>(update);
                _unitOfWork.ClassRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Class>>(delete);
                _unitOfWork.ClassRepository.BulkDelete(listDelete);
            }
        }

        private void UploadColorCode(List<ColorCodeUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<ColorCode>>(insert);
                _unitOfWork.ColorCodeRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<ColorCode>>(update);
                _unitOfWork.ColorCodeRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<ColorCode>>(delete);
                _unitOfWork.ColorCodeRepository.BulkDelete(listDelete);
            }
        }

        private void UploadColumns(List<ColumnUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Columns>>(insert);
                _unitOfWork.ColumnsRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Columns>>(update);
                _unitOfWork.ColumnsRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Columns>>(delete);
                _unitOfWork.ColumnsRepository.BulkDelete(listDelete);
            }
        }

        private void UploadCompany(List<CompanyUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Company>>(insert);
                _unitOfWork.CompanyRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Company>>(update);
                _unitOfWork.CompanyRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Company>>(delete);
                _unitOfWork.CompanyRepository.BulkDelete(listDelete);
            }
        }

        private void UploadCustomers(List<CustomerUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Customers>>(insert);
                _unitOfWork.CustomersRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Customers>>(update);
                _unitOfWork.CustomersRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Customers>>(delete);
                _unitOfWork.CustomersRepository.BulkDelete(listDelete);
            }
        }

        private void UploadDataType(List<DataTypeUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<DataType>>(insert);
                _unitOfWork.DataTypeRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<DataType>>(update);
                _unitOfWork.DataTypeRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<DataType>>(delete);
                _unitOfWork.DataTypeRepository.BulkDelete(listDelete);
            }
        }

        private void UploadJobs(List<JobUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Jobs>>(insert);
                _unitOfWork.JobsRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Jobs>>(update);
                _unitOfWork.JobsRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Jobs>>(delete);
                _unitOfWork.JobsRepository.BulkDelete(listDelete);
            }
        }

        private void UploadPlaces(List<PlaceUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Places>>(insert);
                _unitOfWork.PlacesRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Places>>(update);
                _unitOfWork.PlacesRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Places>>(delete);
                _unitOfWork.PlacesRepository.BulkDelete(listDelete);
            }
        }

        private void UploadPrices(List<PriceUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Prices>>(insert);
                _unitOfWork.PricesRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Prices>>(update);
                _unitOfWork.PricesRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Prices>>(delete);
                _unitOfWork.PricesRepository.BulkDelete(listDelete);
            }
        }

        private void UploadRules(List<RuleUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Rules>>(insert);
                _unitOfWork.RulesRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Rules>>(update);
                _unitOfWork.RulesRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Rules>>(delete);
                _unitOfWork.RulesRepository.BulkDelete(listDelete);
            }
        }

        private void UploadSlots(List<SlotUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Slots>>(insert);
                _unitOfWork.SlotsRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Slots>>(update);
                _unitOfWork.SlotsRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Slots>>(delete);
                _unitOfWork.SlotsRepository.BulkDelete(listDelete);
            }
        }

        private void UploadStaffs(List<StaffUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Staffs>>(insert);
                _unitOfWork.StaffsRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Staffs>>(update);
                _unitOfWork.StaffsRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Staffs>>(delete);
                _unitOfWork.StaffsRepository.BulkDelete(listDelete);
            }
        }

        private void UploadStaffPlace(List<StaffPlaceUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<StaffPlace>>(insert);
                _unitOfWork.StaffPlaceRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<StaffPlace>>(update);
                _unitOfWork.StaffPlaceRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<StaffPlace>>(delete);
                _unitOfWork.StaffPlaceRepository.BulkDelete(listDelete);
            }
        }

        private void UploadSubscriptions(List<SubscriptionUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Subscriptions>>(insert);
                _unitOfWork.SubscriptionsRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Subscriptions>>(update);
                _unitOfWork.SubscriptionsRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Subscriptions>>(delete);
                _unitOfWork.SubscriptionsRepository.BulkDelete(listDelete);
            }
        }

        private void UploadTeamLead(List<TeamLeadUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<TeamLead>>(insert);
                _unitOfWork.TeamLeadRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<TeamLead>>(update);
                _unitOfWork.TeamLeadRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<TeamLead>>(delete);
                _unitOfWork.TeamLeadRepository.BulkDelete(listDelete);
            }
        }

        private void UploadTeamPlaces(List<TeamPlaceUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<TeamPlaces>>(insert);
                _unitOfWork.TeamPlacesRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<TeamPlaces>>(update);
                _unitOfWork.TeamPlacesRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<TeamPlaces>>(delete);
                _unitOfWork.TeamPlacesRepository.BulkDelete(listDelete);
            }
        }

        private void UploadTeams(List<TeamUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Teams>>(insert);
                _unitOfWork.TeamsRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Teams>>(update);
                _unitOfWork.TeamsRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Teams>>(delete);
                _unitOfWork.TeamsRepository.BulkDelete(listDelete);
            }
        }

        private void UploadTeamWorker(List<TeamWorkerUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<TeamWorker>>(insert);
                _unitOfWork.TeamWorkerRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<TeamWorker>>(update);
                _unitOfWork.TeamWorkerRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<TeamWorker>>(delete);
                _unitOfWork.TeamWorkerRepository.BulkDelete(listDelete);
            }
        }

        private void UploadTeamZone(List<TeamZoneUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<TeamZone>>(insert);
                _unitOfWork.TeamZoneRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<TeamZone>>(update);
                _unitOfWork.TeamZoneRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<TeamZone>>(delete);
                _unitOfWork.TeamZoneRepository.BulkDelete(listDelete);
            }
        }

        private void UploadWithdraws(List<WithdrawUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Withdraws>>(insert);
                _unitOfWork.WithdrawsRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Withdraws>>(update);
                _unitOfWork.WithdrawsRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Withdraws>>(delete);
                _unitOfWork.WithdrawsRepository.BulkDelete(listDelete);
            }
        }

        private void UploadWorkerPlace(List<WorkerPlaceUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<WorkerPlace>>(insert);
                _unitOfWork.WorkerPlaceRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<WorkerPlace>>(update);
                _unitOfWork.WorkerPlaceRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<WorkerPlace>>(delete);
                _unitOfWork.WorkerPlaceRepository.BulkDelete(listDelete);
            }
        }

        private void UploadWorkers(List<WorkerUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Workers>>(insert);
                _unitOfWork.WorkersRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Workers>>(update);
                _unitOfWork.WorkersRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Workers>>(delete);
                _unitOfWork.WorkersRepository.BulkDelete(listDelete);
            }
        }

        private void UploadZoneColumn(List<ZoneColumnUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<ZoneColumn>>(insert);
                _unitOfWork.ZoneColumnRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<ZoneColumn>>(update);
                _unitOfWork.ZoneColumnRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<ZoneColumn>>(delete);
                _unitOfWork.ZoneColumnRepository.BulkDelete(listDelete);
            }
        }

        private void UploadZones(List<ZoneUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == 0 && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<Zones>>(insert);
                _unitOfWork.ZonesRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id > 0 && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<Zones>>(update);
                _unitOfWork.ZonesRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id > 0 && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<Zones>>(delete);
                _unitOfWork.ZonesRepository.BulkDelete(listDelete);
            }
        }

        private void UploadUserProfile(List<AppUserUpload> data)
        {
            if (data == null || data.Count <= 0) return;

            var insert = data.FindAll(x => x.Id == string.Empty && x.Action == "Create");
            if (insert.Count > 0)
            {
                var listInsert = _mapper.Map<List<AppUser>>(insert);
                _unitOfWork.UserProfileRepository.BulkInsert(listInsert);
            }

            var update = data.FindAll(x => x.Id != string.Empty && x.Action == "Update");
            if (update.Count > 0)
            {
                var listUpdate = _mapper.Map<List<AppUser>>(update);
                _unitOfWork.UserProfileRepository.BulkUpdate(listUpdate);
            }

            var delete = data.FindAll(x => x.Id != string.Empty && x.Action == "Delete");
            if (delete.Count > 0)
            {
                var listDelete = _mapper.Map<List<AppUser>>(delete);
                _unitOfWork.UserProfileRepository.BulkDelete(listDelete);
            }
        }
    }
}
