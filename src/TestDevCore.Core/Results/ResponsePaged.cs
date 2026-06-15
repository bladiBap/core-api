namespace TestDevCore.Core.Results
{
    public class ResponsePaged<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Data { get; set; }

        public ResponsePaged(IEnumerable<T> data, int totalItems, int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            Data = data;
        }
    }
}
