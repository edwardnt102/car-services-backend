using Messaging.Model;
using System.Threading.Tasks;

namespace Messaging.SMS
{
    public interface ISmsServices
    {
        Task<SmsResponseModel> SendAsync(SmsSendModel model);
    }
}
