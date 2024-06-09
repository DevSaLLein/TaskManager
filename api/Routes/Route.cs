using api.Routes.Tasks;

namespace api.Routes
{
    public static class Route
    {
        public static void MapController(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("/")
                .WithTags("Health Check")
                .MapGet("/", () => new { message =  "Ok" })
            ;

            endpoints.MapGroup("/v1/Tasks")
                .WithTags("Tasks here!")
                .MapEndpoint<CreateTaskRoute>()
                .MapEndpoint<GetAllTasksRoute>()
            ;
        }
        
        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>
        (this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}