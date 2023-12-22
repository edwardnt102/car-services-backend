namespace Authentication.Model
{
    public class AccountQueryItem
    {
        public string id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string role { get; set; }
        public string avatar { get; set; }
        public bool active { get; set; }
        public string fullName { get; set; }
    }
}
