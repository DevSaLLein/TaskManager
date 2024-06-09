namespace api
{
    public static class ApiConfiguration
    {
        public static string ConnectionString { get; set; } = string.Empty; 

        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 10;
    }
}