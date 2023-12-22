using Authentication.Authentication.Commands;
using Authentication.Model;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Interfaces;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using ViewModel.RequestModel.Account;

namespace l2404.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginCommand _loginCommand;
        private readonly IAccountServices _accountServices;

        public AuthenticationController(ILoginCommand loginCommand, IAccountServices accountServices)
        {
            _loginCommand = loginCommand;
            _accountServices = accountServices;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginWeb([FromBody] LoginRequestModel credentials)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                return Ok(await _loginCommand.Authenticate(credentials, "isWeb"));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " AuthenticationController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpPost("LoginMobile")]
        public async Task<IActionResult> LoginMobile([FromBody] LoginRequestModel credentials)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                return Ok(await _loginCommand.Authenticate(credentials, "isMobile"));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " AuthenticationController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpPost("update-avatar")]
        [Authorize]
        public async Task<IActionResult> UpdateAvatarAsync([FromForm] AvatarUpdateModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                if (request == null)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelIsInvalid);
                }
                return Ok(await _accountServices.UpdateAvatarAsync(request.Files));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " AuthenticationController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpPost("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfileAsync([FromForm] ProfileUpdateModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                if (request == null)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelIsInvalid);
                }
                return Ok(await _accountServices.UpdateProfileAsync(request));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " AuthenticationController: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }
                if (request == null)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelIsInvalid);
                }
                return Ok(await _accountServices.ChangePassword(request));
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " change-password: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }

        [HttpGet("get-profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.ErrorMessageCodes.ModelStateMessage);
                }

                return Ok(await _accountServices.GetProfileDetail());
            }
            catch (Exception ex)
            {
                Log.Fatal(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " GetProfile: " + ex.Message);
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), Constants.ErrorMessageCodes.InternalServerErrorMessage);
            }
        }
    }
}
