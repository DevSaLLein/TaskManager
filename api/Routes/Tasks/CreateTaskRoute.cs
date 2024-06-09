using System.Security.Claims;
using api.DTO.Tarefa.Request;
using api.DTO.Tarefa.Response;
using TasManager.Extensions;

namespace api.Routes.Tasks
{
    public class CreateTaskRoute : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", HandleAsync)
                .WithName("Create a new Task to you")
                .WithSummary("Create a new Task here!")
                .WithDescription("Enter a new body to create a new Task to you!")
                .WithOrder(1)
                .Produces<TarefaResponse>()
            ;
        }

        private static async Task<IResult> HandleAsync
        (ITaskService service, TarefaCreateRequest request, ClaimsPrincipal user, CancellationToken token)
        {
            var response = await service.CreateTask(request, user.GetUsername(), token);

            return TypedResults.Created($"/v1/tasks/{response.Id}", response);
        }
    }
}