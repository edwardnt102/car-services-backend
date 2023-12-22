using Authentication.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.Authentication.Commands.Factory
{
    public interface ITokenFactory
    {
        Task<string> GenerateJwt(ClaimsIdentity identity,
                                 IJwtFactory jwtFactory,
                                 string userName,
                                 JwtIssuerOptions jwtOptions,
                                 JsonSerializerSettings serializerSettings,
                                 string pictureUrl,
                                 string fullName,
                                 string role,
                                 List<Claim> claims = null
                                 );
    }
}
