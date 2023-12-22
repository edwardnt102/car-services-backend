using System.Collections.Generic;

namespace Authentication.Common
{
    public class CommonResponse
    {
        public string Error_code { get; set; }
        public string Error_message { get; set; }
    }
    public class ResponseModelType<T> : CommonResponse
    {
        public T Data { get; set; }
    }

    public class ResponseListModelType<T> : CommonResponse
    {
        public List<T> ListData { get; set; }
    }
    public class ResponseListModelWithTotalType<T> : ResponseListModelType<T>
    {
        public int Total { get; set; }
    }
}
