using Authentication.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.Authentication.Commands.Factory
{
    public class TokenFactory : ITokenFactory
    {
        public async Task<string> GenerateJwt(ClaimsIdentity identity,
                                              IJwtFactory jwtFactory,
                                              string userName,
                                              JwtIssuerOptions jwtOptions,
                                              JsonSerializerSettings serializerSettings,
                                              string pictureUrl,
                                              string fullName,
                                              string role,
                                              List<Claim> claims = null)
        {
            var response = new AuthenticateResponse
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                Token = await jwtFactory.GenerateEncodedToken(userName, identity, claims),
                ExpireTime = (int)jwtOptions.ValidFor.TotalSeconds,
                AvatarUrl = pictureUrl,
                FullName = fullName,
                Role = role,
                Username = userName
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }

    }
}
