using Authentication.Authentication.Commands.Factory;
using Authentication.Model;
using Entity.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.Authentication.Commands
{
    public class ClaimsCommand : IClaimsCommand
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtFactory _jwtFactory;

        public ClaimsCommand(UserManager<AppUser> userManager, IJwtFactory jwtFactory)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
        }

        public async Task<ClaimsIdentity> GetClaimsIdentity(LoginRequestModel loginRequestModel)
        {
            if (string.IsNullOrEmpty(loginRequestModel.UserName) || string.IsNullOrEmpty(loginRequestModel.Password))
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
