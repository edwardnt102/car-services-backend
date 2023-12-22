using System.Collections.Generic;

namespace Common.Pagging
{
    public class PagedResult<T> : IPagedResult<T>
    {
        public T Data { get; set; }
        public string ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
    }
    public class PagedResults<T> : IPagedResults<T>
    {
        public PagedResults() { }
        public PagedResults(IEnumerable<T> listData, int totalCount)
        {
            ListData = listData;
            TotalRecords = totalCount;
        }

        public int TotalRecords { get; set; }
        public IEnumerable<T> ListData { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}
