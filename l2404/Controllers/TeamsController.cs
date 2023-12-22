using Common.Constants;
using Common.Pagging;
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
using ViewModel.RequestModel.Teams;

namespace l2404.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsServices _teamsServices;

        public TeamsController(ITeamsServices teamsServices)
        {
            _teamsServices = teamsServices;
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

                var result = await _teamsServices.GetAllAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " TeamsController: " + ex.Message);
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
                return Ok(await _teamsServices.SuggestionAsync(textSearch));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " TeamsController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveAsync([FromForm] TeamSaveModel model, List<IFormFile> files)
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
                return Ok(await _teamsServices.SaveAsync(model, files));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " TeamsController: " + ex.Message);
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
                return Ok(await _teamsServices.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " TeamsController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpGet("SuggestionZoneByPlace")]
        public IActionResult SuggestionZoneByPlaceAsync(string textSearch, string listTeamPlace)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                return Ok(_teamsServices.SuggestionZoneByPlaceAsync(textSearch, listTeamPlace));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " TeamsController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpGet("SuggestionWorkerByPlace")]
        public IActionResult SuggestionWorkerByPlaceAsync(string textSearch, string listTeamPlace)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                return Ok(_teamsServices.SuggestionWorkerByPlaceAsync(textSearch, listTeamPlace));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " TeamsController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }
    }
}
