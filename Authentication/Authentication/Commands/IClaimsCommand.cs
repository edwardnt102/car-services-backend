
using Authentication.Model;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.Authentication.Commands
{
    public interface IClaimsCommand
    {
        Task<ClaimsIdentity> GetClaimsIdentity(LoginRequestModel loginRequestModel);
    }
}
