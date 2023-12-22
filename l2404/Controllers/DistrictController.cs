using Common.Constants;
using Common.Pagging;
using Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace l2404.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictServices _districtServices;

        public DistrictController(IDistrictServices districtServices)
        {
            _districtServices = districtServices;
        }

        [HttpPost("BulkInsert")]
        public async Task<IActionResult> BulkInsertAsync(IEnumerable<District> models)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (null == models)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelIsInvalidMessage);
                }
                return Ok(await _districtServices.BulkInsertAsync(models));
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                return StatusCode(Constants.ErrorMessageCodes.InternalServerError, Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PagingRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _districtServices.GetAllAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " DistrictController: " + ex.Message);
                return StatusCode(Constants.ErrorMessageCodes.InternalServerError, Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }

        }

        [HttpGet("Suggestion")]
        public async Task<IActionResult> SuggestionAsync(string textSearch, long provinceId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _districtServices.SuggestionAsync(textSearch, provinceId));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " DistrictController: " + ex.Message);
                return StatusCode(Constants.ErrorMessageCodes.InternalServerError, Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<District>> UpdateAsync(District model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (null == model)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelIsInvalidMessage);
                }
                if (model.Id == 0)
                {
                    return Ok(await _districtServices.InsertAsync(model));
                }
                return Ok(await _districtServices.UpdateAsync(model));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " DistrictController: " + ex.Message);
                return StatusCode(Constants.ErrorMessageCodes.InternalServerError, Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await _districtServices.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " DistrictController: " + ex.Message);
                return StatusCode(Constants.ErrorMessageCodes.InternalServerError, Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }
    }

}
