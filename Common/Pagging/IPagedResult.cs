using System.Collections.Generic;

namespace Common.Pagging
{
    public interface IPagedResults<T>
    {
        IEnumerable<T> ListData { get; set; }
        int TotalRecords { get; set; }
        string ResponseMessage { get; set; }
        int ResponseCode { get; set; }
    }
    public interface IPagedResult<T>
    {
        T Data { get; set; }
        string ResponseMessage { get; set; }
        int ResponseCode { get; set; }
    }
}
