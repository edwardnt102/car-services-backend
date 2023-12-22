namespace Common.Shared
{
    public interface IAuthenticatedUserService
    {
        public string UserId { get; }
        public string Username { get; }
        public long CompanyId { get; }
    }
}
