using HealthMed.Grupo27.API.Security;
using HealthMed.Grupo27.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HealthMed.Grupo27.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoRepository _repository;
        private readonly ILogger<ConsultaController> _logger;

        public MedicoController(IMedicoRepository repository, ILogger<ConsultaController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Busca médicos cadastrados com base em filtros opcionais.
        /// </summary>
        /// <param name="especialidade">Especialidade do médico (opcional).</param>
        /// <param name="nome">Nome do médico (opcional).</param>
        /// <param name="crm">CRM do médico (opcional).</param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeProfiles(UserProfile.Medico, UserProfile.Administrador, UserProfile.Paciente)]
        public async Task<IActionResult> BuscarMedicos([FromQuery] string? especialidade, [FromQuery] string? nome, [FromQuery] string? crm, [FromQuery] decimal? valorConsultaInicial, [FromQuery] decimal? valorConsultaFinal)
        {
            try
            {
                _logger.LogInformation("Iniciando a busca de medicos.");

                var medicos = await _repository.GetMedicosAsync();

                if (!string.IsNullOrEmpty(especialidade))
                {
                    medicos = medicos.Where(m => m.Especialidade.Contains(especialidade, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(nome))
                {
                    medicos = medicos.Where(m => m.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(crm))
                {
                    medicos = medicos.Where(m => m.CRM.Equals(crm, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (valorConsultaInicial.HasValue)
                {
                    medicos = medicos.Where(m => m.ValorConsulta >= valorConsultaInicial.Value).ToList();
                }

                if (valorConsultaFinal.HasValue)
                {
                    medicos = medicos.Where(m => m.ValorConsulta <= valorConsultaFinal.Value).ToList();
                }

                return Ok(medicos);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex, "Erro ao buscar medicos.");
                return BadRequest($"Error: {Ex.Message}");
            }
        }
    }
}
