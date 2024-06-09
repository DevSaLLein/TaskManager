namespace api.DTO
{
    public class PagedRequest
    {
        public string UserName { get; set; }
        public int PageSize { get; set; } = ApiConfiguration.DefaultPageSize;
        public int PageNumber { get; set; } = ApiConfiguration.DefaultPageNumber;
    }
}