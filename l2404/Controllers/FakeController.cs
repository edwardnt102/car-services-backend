using Common.Pagging;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace l2404.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeController : ControllerBase
    {
        private readonly IDistrictServices _districtServices;
        private readonly IProjectServices _projectServices;
        private readonly IWardServices _wardServices;
        private readonly IStreetServices _streetServices;
        private readonly IProvinceServices _provinceServices;
        private readonly IUploadFileServices _uploadFileServices;

        public FakeController(IDistrictServices districtServices, IProjectServices projectServices,
            IWardServices wardServices, IStreetServices streetServices, IProvinceServices provinceServices, IUploadFileServices uploadFileServices)
        {
            _districtServices = districtServices;
            _projectServices = projectServices;
            _wardServices = wardServices;
            _streetServices = streetServices;
            _provinceServices = provinceServices;
            _uploadFileServices = uploadFileServices;
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFileAsync([FromForm] FileModel model)
        {
            return Ok(await _uploadFileServices.UploadFileAsync(model.File));
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<string>> GetAllAsync()
        {
            var lst = new List<string>();
            using (StreamReader file = new StreamReader(@"E:\WOR-FOR-FOOD\CAR_SERVICES\SourceCode\car-services\l2404\wwwroot\data\district.txt"))
            {
                string ln;
                var d = new List<District>();
                long tutu = 0;
                while ((ln = file.ReadLine()) != null)
                {
                    ln = ln.Substring(1, ln.Length - 3);
                    var district = ln.Split(",");
                    try
                    {
                        tutu = long.Parse(district[0]);
                        var entity = new District()
                        {
                            Id = long.Parse(district[0]),
                            Name = district[1].Replace("'", ""),
                            Prefix = district[2].Replace("'", ""),
                            ProvinceId = long.Parse(district[3]),
                            DistrictCode = long.Parse(district[0])
                        };
                        d.Add(entity);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(tutu);
                        throw;
                    }

                }

                var a = await _districtServices.GetAllAsync(new PagingRequest());
                if (a.ListData == null || a.ListData.ToList().Count() <= 0)
                {
                    await _districtServices.BulkInsertAsync(d);
                }

                file.Close();
            }
            using (StreamReader file = new StreamReader(@"E:\WOR-FOR-FOOD\CAR_SERVICES\SourceCode\car-services\l2404\wwwroot\data\project.txt"))
            {
                string ln;
                var d = new List<Project>();
                long tutu = 0;
                while ((ln = file.ReadLine()) != null)
                {
                    ln = ln.Substring(1, ln.Length - 3);
                    var district = ln.Split(",");
                    try
                    {
                        tutu = long.Parse(district[0]);
                        var entity = new Project()
                        {
                            Id = long.Parse(district[0]),
                            ProjectCode = int.Parse(district[0]),
                            Name = district[1].Replace("'", "").Replace("=", ", "),
                            ProvinceId = long.Parse(district[2]),
                            DistrictId = long.Parse(district[3]),
                            Lat = double.Parse(district[4]),
                            Lng = double.Parse(district[5])
                        };
                        d.Add(entity);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(tutu);
                        throw;
                    }
                }

                var a = await _projectServices.GetAllAsync(new PagingRequest());
                if (a.ListData == null || a.ListData.ToList().Count() <= 0)
                {
                    await _projectServices.BulkInsertAsync(d);
                }
                file.Close();
            }
            using (StreamReader file = new StreamReader(@"E:\WOR-FOR-FOOD\CAR_SERVICES\SourceCode\car-services\l2404\wwwroot\data\province.txt"))
            {
                string ln;
                var d = new List<Province>();

                while ((ln = file.ReadLine()) != null)
                {
                    ln = ln.Substring(1, ln.Length - 3);
                    var district = ln.Split(",");
                    var entity = new Province()
                    {
                        Id = int.Parse(district[0]),
                        ProvinceCode = int.Parse(district[0]),
                        Name = district[1].Replace("'", "").Replace("=", ", "),
                        Code = district[2].Replace("'", "").Replace("=", ", "),
                    };
                    d.Add(entity);
                }

                var a = await _provinceServices.GetAllAsync(new PagingRequest());
                if (a.ListData == null || a.ListData.ToList().Count() <= 0)
                {
                    await _provinceServices.BulkInsertAsync(d);
                }
                file.Close();
            }
            using (StreamReader file = new StreamReader(@"E:\WOR-FOR-FOOD\CAR_SERVICES\SourceCode\car-services\l2404\wwwroot\data\street.txt"))
            {
                string ln;
                var d = new List<Street>();
                long tutu = 0;
                while ((ln = file.ReadLine()) != null)
                {
                    ln = ln.Substring(1, ln.Length - 3);
                    var district = ln.Split(",");
                    try
                    {
                        tutu = long.Parse(district[0]);

                        var entity = new Street()
                        {
                            Id = long.Parse(district[0]),
                            StreetCode = int.Parse(district[0]),
                            Name = district[1].Replace("'", "").Replace("=", ", "),
                            Prefix = district[2].Replace("'", "").Replace("=", ", "),
                            ProvinceId = long.Parse(district[3]),
                            DistrictId = long.Parse(district[4]),
                        };
                        d.Add(entity);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(tutu);
                        throw;
                    }


                }

                var a = await _streetServices.GetAllAsync(new PagingRequest());
                if (a.ListData == null || a.ListData.ToList().Count() <= 0)
                {
                    await _streetServices.BulkInsertAsync(d);
                }
                file.Close();
            }
            using (StreamReader file = new StreamReader(@"E:\WOR-FOR-FOOD\CAR_SERVICES\SourceCode\car-services\l2404\wwwroot\data\ward.txt"))
            {
                string ln;
                var d = new List<Ward>();
                long tutu = 0;
                while ((ln = file.ReadLine()) != null)
                {
                    ln = ln.Substring(1, ln.Length - 3);
                    var district = ln.Split(",");

                    try
                    {
                        tutu = int.Parse(district[0]);
                        var entity = new Ward()
                        {
                            Id = int.Parse(district[0]),
                            WardCode = int.Parse(district[0]),
                            Name = district[1].Replace("'", "").Replace("=", ", "),
                            Prefix = district[2].Replace("'", "").Replace("=", ", "),
                            ProvinceId = int.Parse(district[3]),
                            DistrictId = int.Parse(district[4]),
                        };
                        d.Add(entity);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(tutu);
                        throw;
                    }

                }

                var a = await _wardServices.GetAllAsync(new PagingRequest());
                if (a.ListData == null || a.ListData.ToList().Count() <= 0)
                {
                    await _wardServices.BulkInsertAsync(d);
                }
                file.Close();
            }
            return lst;
        }
    }
}
