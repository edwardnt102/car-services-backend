using Common.Shared;
using Microsoft.AspNetCore.Http;
using Repository;
using System.Security.Claims;

namespace l2404.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        private readonly IDapperRepository _dapperRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor, IDapperRepository dapperRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _dapperRepository = dapperRepository;
        }

        public string UserId
        {
            get
            {
                var firstUser = _dapperRepository.QueryFirstOrDefault<string>("select top 1 id from UserProfile") ?? string.Empty;
                return _httpContextAccessor.HttpContext?.User?.FindFirstValue("id") ?? firstUser;
            }
        }

        public string Username => _dapperRepository.QueryFirstOrDefault<string>($"select top 1 FullName from UserProfile where Id='{this.UserId}'") ?? string.Empty;

        public long CompanyId
        {
            get
            {
                var worker = _dapperRepository.QueryFirstOrDefault<long?>($"select top 1 CompanyId from Workers where UserId='{this.UserId}'");
                if (worker != null && worker != 0)
                {
                    return worker ?? 0;
                }
                var Customers = _dapperRepository.QueryFirstOrDefault<long?>($"select top 1 CompanyId from Customers where UserId='{this.UserId}'");
                if (Customers != null && Customers != 0)
                {
                    return Customers ?? 0;
                }
                var Staffs = _dapperRepository.QueryFirstOrDefault<long?>($"select top 1 CompanyId from Staffs where UserId='{this.UserId}'");
                if (Staffs != null && Staffs != 0)
                {
                    return Customers ?? 0;
                }
                return 0;
            }
        }
    }
}
