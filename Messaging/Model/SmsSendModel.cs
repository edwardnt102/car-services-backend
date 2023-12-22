namespace Messaging.Model
{
    public class SmsSendModel
    {
        public string BrandName { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
    public class SmsSendRrequestModel
    {
        public string BrandName { get; set; }
        public string access_token { get; set; }
        public string session_id { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }

}
