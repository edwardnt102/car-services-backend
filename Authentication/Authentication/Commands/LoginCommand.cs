using Authentication.Authentication.Commands.Factory;
using Authentication.Common;
using Authentication.Model;
using Entity.DBContext;
using Entity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.Authentication.Commands
{
    public class LoginCommand : ILoginCommand
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenFactory _tokenFactory;
        private readonly DatabaseContext _authenticationContext;


        public LoginCommand(UserManager<AppUser> userManager,
                            DatabaseContext authentication,
                            IJwtFactory jwtFactory,
                            RoleManager<IdentityRole> roleManager,
                            ITokenFactory tokenFactory,
                            IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _authenticationContext = authentication;
            _jwtFactory = jwtFactory;
            _roleManager = roleManager;
            _tokenFactory = tokenFactory;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<ResponseModelType<AuthenticateResponse>> Authenticate(LoginRequestModel model, string type)
        {
            var claims = new List<Claim>();
            var returnRole = "";
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var userModel = _authenticationContext.Users.FirstOrDefault(c => c.Id == user.Id);
                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    return new ResponseModelType<AuthenticateResponse> { Error_code = Constants.ErrorMessageCodes.UserInvalid, Error_message = Constants.ErrorMessageCodes.UserInvalidMessage };
                }
                if (userModel == null || userModel.IsDeleted)
                {
                    return new ResponseModelType<AuthenticateResponse> { Error_code = Constants.ErrorMessageCodes.UserNotFound, Error_message = Constants.ErrorMessageCodes.UserNotFoundMessage };
                }
                if (userModel.Active == false)
                {
                    return new ResponseModelType<AuthenticateResponse> { Error_code = Constants.ErrorMessageCodes.UserInActive, Error_message = Constants.ErrorMessageCodes.UserUserInActiveMessage };
                }
                var userClaims = await _userManager.GetClaimsAsync(user);
                var userRoles = await _userManager.GetRolesAsync(user);
                returnRole = userRoles[0];
                if ((returnRole == "Worker" || returnRole == "Customer") && type == "isWeb")
                {
                    return new ResponseModelType<AuthenticateResponse> { Error_code = Constants.ErrorMessageCodes.UnauthorizedUser, Error_message = Constants.ErrorMessageCodes.UnauthorizedUserMessage };
                }
                claims.AddRange(userClaims);
                foreach (var userRole in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                    var role = await _roleManager.FindByNameAsync(userRole);
                    if (role == null) continue;
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            var identity = await GetClaimsIdentity(model);
            var userSelected = GetUserName(user?.Id);
            var fullname = userSelected.fullName;
            var jwt = await _tokenFactory.GenerateJwt(identity,
                                                      _jwtFactory,
                                                      model.UserName,
                                                      _jwtOptions,
                                                      new JsonSerializerSettings { Formatting = Formatting.Indented },
                                                      userSelected.avatar,
                                                      fullname,
                                                      returnRole,
                                                      claims);
            var jwtObject = JsonConvert.DeserializeObject<AuthenticateResponse>(jwt);


            return new ResponseModelType<AuthenticateResponse>
            {
                Error_code = Constants.ErrorMessageCodes.Success,
                Error_message = Constants.ErrorMessageCodes.SuccessMessage,
                Data = jwtObject
            };
        }
        private AccountQueryItem GetUserName(string id)
        {
            var result = new AccountQueryItem();
            var systemUser = _authenticationContext.Users.FirstOrDefault(x => x.Id == id);
            result.user_name = systemUser?.UserName;
            result.email = systemUser?.Email;
            result.avatar = systemUser?.PictureUrl;
            result.fullName = systemUser?.FullName;
            return result;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(LoginRequestModel loginRequestModel)
        {
            if (string.IsNullOrEmpty(loginRequestModel?.UserName) || string.IsNullOrEmpty(loginRequestModel?.Password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(loginRequestModel.UserName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, loginRequestModel.Password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(loginRequestModel.UserName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
