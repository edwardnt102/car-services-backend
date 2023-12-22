namespace Common.Pagging
{
    public class PagingRequest
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string SortField { get; set; }
        public string SortDir { get; set; }
        public string SearchText { get; set; }
    }
    public class PagingNoSortRequest
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
    public class PagingNoSortRequestWithSearch<T>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public T SearchData { get; set; }
    }
}
