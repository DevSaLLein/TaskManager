using Microsoft.AspNetCore.Mvc;
using TaskManager.DTO;

namespace ConsumoDeAPIs.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ViaCepController(IViaCepIntegracao viaCep) : ControllerBase
    {
        private readonly IViaCepIntegracao _viaCep = viaCep;

        [HttpGet("{cep}")]
        public async Task<ActionResult<ViaCepResponse>> ObterDadosViaCep(string cep)
        {
            var responseData = await _viaCep.ObterDadosViaCep(cep);

            if(responseData == null) return BadRequest("Cep não encontrado");

            return Ok(responseData);
        }
    }
}