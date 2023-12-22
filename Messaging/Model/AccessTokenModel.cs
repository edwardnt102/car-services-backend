namespace Messaging.Model
{
    public class AccessTokenModel
    {
        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string scope { get; set; }
        public string session_id { get; set; }
    }

    public class AccessTokenRequestModel
    {
        public string scope { get; set; }
        public string session_id { get; set; }
    }
}
