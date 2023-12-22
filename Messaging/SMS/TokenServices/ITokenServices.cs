using Messaging.Model;
using System.Threading.Tasks;

namespace Messaging.SMS.TokenServices
{
    public interface ITokenServices
    {
        Task<AccessTokenResponseModel> SendAsync(AccessTokenRequestModel model);
    }
}
