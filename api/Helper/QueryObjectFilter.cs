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

// using TaskManager.Enum;

// namespace TaskManager.Helpers
// {
//     public static class QueryObjectFilter
//     {
//         public static StatusEnum? Status = null;
//         public static bool isSortByData = false;
//         public const int PageNumber = 1;
//         public const int PageSize = 20;
//     }
// }