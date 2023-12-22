using Messaging.Model;
using Messaging.SMS.TokenServices;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.SMS
{
    public class SmsServices : ISmsServices
    {
        private readonly ITokenServices _tokenServices;
        public SmsServices(ITokenServices tokenServices)
        {
            _tokenServices = tokenServices;
        }

        public async Task<SmsResponseModel> SendAsync(SmsSendModel model)
        {
            var sessionId = Guid.NewGuid().ToString();
            var tokenRequestAccess = new AccessTokenRequestModel
            {
                session_id = sessionId,
                scope = "send_brandname_otp"
            };
            var tokenObj = await _tokenServices.SendAsync(tokenRequestAccess);

            using (var httpClient = new HttpClient())
            {
                var reservation = new SmsSendRrequestModel()
                {
                    session_id = sessionId,
                    access_token = tokenObj.access_token,
                    BrandName = model.BrandName,
                    Message = Base64Encode(model.Message),
                    Phone = model.Phone
                };
                var json = JsonConvert.SerializeObject(reservation);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://app.sms.fpt.net/api/push-brandname-otp", data))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var reservationList = JsonConvert.DeserializeObject<SmsResponseModel>(apiResponse);
                    return reservationList;
                }

            }

        }
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
