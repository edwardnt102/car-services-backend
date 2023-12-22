using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.Authentication.Commands.Factory
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, List<Claim> additionalClaims);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
