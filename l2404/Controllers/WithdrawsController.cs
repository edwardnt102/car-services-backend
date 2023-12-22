using Common.Constants;
using Common.Pagging;
using Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Interfaces;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace l2404.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WithdrawsController : ControllerBase
    {
        private readonly IWithdrawsServices WithdrawsServices;
        public WithdrawsController(IWithdrawsServices WithdrawsServices)
        {
            this.WithdrawsServices = WithdrawsServices;
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

                var result = await WithdrawsServices.GetAllAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WithdrawsController: " + ex.Message);
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
                return Ok(await WithdrawsServices.SuggestionAsync(textSearch));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WithdrawsController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpPost("Save")]
        public async Task<ActionResult<Withdraws>> SaveAsync(Withdraws model)
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
                    return Ok(await WithdrawsServices.InsertAsync(model));
                }
                return Ok(await WithdrawsServices.UpdateAsync(model));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WithdrawsController: " + ex.Message);
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
                return Ok(await WithdrawsServices.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " WithdrawsController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }
    }
}
