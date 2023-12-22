using Common.Constants;
using Common.Pagging;
using Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace l2404.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly ICarsServices _carsServices;

        public CarsController(ICarsServices carsServices)
        {
            _carsServices = carsServices;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PagingRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                if (null == request)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelIsInvalidMessage);
                }

                var result = await _carsServices.GetAllAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CarsController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }

        }

        [HttpGet("Suggestion")]
        public async Task<IActionResult> SuggestionAsync(string textSearch)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                return Ok(await _carsServices.SuggestionAsync(textSearch));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CarsController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveAsync([FromForm] Cars model, List<IFormFile> files, List<IFormFile> carImage)
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
                return Ok(await _carsServices.SaveAsync(model, files, carImage));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CarsController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                return Ok(await _carsServices.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CarsController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpGet("SuggestionCarModelByCarBrand")]
        public IActionResult SuggestionCarModelByCarBrandAsync(string textSearch, long brandId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                return Ok(_carsServices.SuggestionCarModelByCarBrandAsync(textSearch, brandId));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " CarsController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);


            }
        }
    }
}