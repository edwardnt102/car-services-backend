using Authentication.Common;
using Authentication.Model;
using System.Threading.Tasks;

namespace Authentication.Authentication.Commands
{
    public interface ILoginCommand
    {
        Task<ResponseModelType<AuthenticateResponse>> Authenticate(LoginRequestModel model, string type);
    }
}
