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
using System.Net;
using System.Threading.Tasks;

namespace l2404.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StreetController : ControllerBase
    {
        private readonly IStreetServices _streetServices;

        public StreetController(IStreetServices streetServices)
        {
            _streetServices = streetServices;
        }

        [HttpPost("BulkInsert")]
        public async Task<IActionResult> BulkInsertAsync(IEnumerable<Street> models)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                return Ok(await _streetServices.BulkInsertAsync(models));
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
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

                var result = await _streetServices.GetAllAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " StreetController: " + ex.Message);
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
                return Ok(await _streetServices.SuggestionAsync(textSearch));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " StreetController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpPost("Save")]
        public async Task<ActionResult<Street>> SaveAsync(Street model)
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
                if (model.Id == 0)
                {
                    return Ok(await _streetServices.InsertAsync(model));
                }
                return Ok(await _streetServices.UpdateAsync(model));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " StreetController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                return Ok(await _streetServices.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " StreetController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }
    }
}
