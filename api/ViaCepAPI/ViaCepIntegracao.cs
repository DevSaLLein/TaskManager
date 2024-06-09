using TaskManager.DTO;

namespace ConsumoDeAPIs
{
    public class ViaCepIntegracao(IViaCepIntegracaoRefit refit) : IViaCepIntegracao
    {
        private readonly IViaCepIntegracaoRefit _refit = refit;

        public async Task<ViaCepResponse> ObterDadosViaCep(string cep)
        {
            var responseData = await _refit.ObterDadosViaCep(cep);

            if(responseData != null && responseData.IsSuccessStatusCode) return responseData.Content;
            
            return null;
        }
    }
}