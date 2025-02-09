using HealthMed.Grupo27.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HealthMed.Grupo27.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepository _repository;
        private readonly ILogger<ConsultaController> _logger;

        public PacienteController(IPacienteRepository repository, ILogger<ConsultaController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Obtém a lista de pacientes cadastrados.
        /// </summary>
        /// <returns>Retorna uma lista de pacientes.</returns>
        [HttpGet]
        public async Task<IActionResult> GetPacientes()
        {
            try
            {
                _logger.LogInformation("Iniciando a busca de pacientes.");
                return Ok(await _repository.GetPacientesAsync());
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex, "Erro ao buscar pacientes.");
                return BadRequest($"Error: {Ex.Message}");
            }
            
        }
    }
}
