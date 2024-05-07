using TaskManager.Enum;

namespace TaskManager.Helpers
{
    public class QueryObjectFilter
    {
        public StatusEnum? Status { get; set; } = null;
        public bool isSortByData { get; set; } = false;
    }
}