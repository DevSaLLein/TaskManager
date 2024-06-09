using System.Security.Claims;
using api.DTO;
using api.DTO.Tarefa.Response;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Model;
using TasManager.Extensions;

namespace api.Routes.Tasks
{
    public class GetAllTasksRoute : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                .WithDescription("Get yours tasks here!")
                .Produces<GetAllResponsePaged<TaskItem>>()
            ;
        }

        private static async Task<IResult> HandleAsync
        (
            ITaskService service, 
            ClaimsPrincipal user, 
            CancellationToken token,

            [FromQuery] int pageSize = ApiConfiguration.DefaultPageSize,
            [FromQuery] int PageNumber = ApiConfiguration.DefaultPageNumber
        )
        {
            var request = new PagedRequest
            {
                UserName = user.GetUsername(),
                PageNumber = PageNumber,
                PageSize = pageSize
            };

            var response = await service.GetAllTasksFromUser(request);
            
            return TypedResults.Ok(response);
        } 
    }
}