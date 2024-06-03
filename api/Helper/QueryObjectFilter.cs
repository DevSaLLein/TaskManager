using TaskManager.Enum;

namespace TaskManager.Helpers
{
    public class QueryObjectFilter
    {
        public StatusEnum? Status { get; set; } = null;
        public bool isSortByData { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}