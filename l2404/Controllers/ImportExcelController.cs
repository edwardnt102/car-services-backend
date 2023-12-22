using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Interfaces;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using ViewModel.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace l2404.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportExcelController : ControllerBase
    {
        private readonly IImportExcelServices _importExcelServices;
        public ImportExcelController(IImportExcelServices importExcelServices)
        {
            _importExcelServices = importExcelServices;
        }

        // POST api/<ImportExcelController>
        [HttpPost("/api/import/uploaded")]
        public async Task<IActionResult> UploadAsync([FromForm] FileModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                if (null == model)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelIsInvalidMessage);
                }
                return Ok(await _importExcelServices.UploadAsync(model));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " ImportExcelController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }
    }
}
