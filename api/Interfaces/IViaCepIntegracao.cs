using TaskManager.DTO;

namespace ConsumoDeAPIs.Integration.Interfaces
{
    public interface IViaCepIntegracao
    {
        Task<ViaCepResponse> ObterDadosViaCep(string cep);
    }
}