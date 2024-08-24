namespace Customer.DTOs.Common
{
    public class PaginatedResult
    {
        public IEnumerable<object> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
