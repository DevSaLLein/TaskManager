using Refit;
using TaskManager.DTO;

namespace ConsumoDeAPIs.Integration.Response.Refit
{
    public interface IViaCepIntegracaoRefit
    {
        [Get("/ws/{cep}/json")]
        Task<ApiResponse<ViaCepResponse>> ObterDadosViaCep(string cep);
    }
}