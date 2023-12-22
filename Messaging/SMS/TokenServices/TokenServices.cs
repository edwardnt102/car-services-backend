using Messaging.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.SMS.TokenServices
{
    public class TokenServices : ITokenServices
    {
        public async Task<AccessTokenResponseModel> SendAsync(AccessTokenRequestModel model)
        {
            using (var httpClient = new HttpClient())
            {
                var reservation = new AccessTokenModel()
                {
                    //client_id = "B5113a3Dd459b801E4c5bd7Bbaf9b0e56Ef9a8A8",
                    //client_secret = "892aC2622999db6c82aec93cfb6ad8cF1458977664e841337edd26e68a29f88b14c76703",
                    client_id = "ce13ea18ad90F8871dfe17F09A4951Ad5eB933f8",
                    client_secret = "C01d2736abf14e8a235aae4229b98dfb7bfe80Ef43e50B56d669c4eEabe529eb78339c3e",
                    grant_type = "client_credentials",
                    scope = model.scope,
                    session_id = model.session_id
                };
                var json = JsonConvert.SerializeObject(reservation);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://app.sms.fpt.net/oauth2/token", data))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var reservationList = JsonConvert.DeserializeObject<AccessTokenResponseModel>(apiResponse);
                    return reservationList;
                }

            }
        }
    }
}
