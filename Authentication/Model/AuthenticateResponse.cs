namespace Authentication.Model
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public int ExpireTime { get; set; }
    }
}
