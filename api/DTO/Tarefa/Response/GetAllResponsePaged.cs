using TaskManager.DTO;
using TasManager.DTO.Account.Response;

namespace api.DTO.Tarefa.Response
{
    public class GetAllResponsePaged<TData>
    {
        
        public GetAllResponsePaged
        (
            UserInformationsToTarefas user,
            ViaCepResponse localidade,
            TData data,
            int totalCount,
            int currentPage = 1,
            int pageSize = ApiConfiguration.DefaultPageSize
        )
        {
            User = user;
            Localidade = localidade;
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public UserInformationsToTarefas User { get; set; }
        public ViaCepResponse Localidade { get; set; }

        public TData Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int) Math.Ceiling(TotalCount / (double) PageSize);
        public int PageSize { get; set; } = ApiConfiguration.DefaultPageSize;
        public int TotalCount { get; set; }
    }
}