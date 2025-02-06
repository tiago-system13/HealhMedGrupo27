using HealthMed.Grupo27.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Grupo27.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoRepository _repository;
        public MedicoController(IMedicoRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> BuscarMedicos([FromQuery] string? especialidade, [FromQuery] string? nome, [FromQuery] string? crm)
        {
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

            return Ok(medicos);
        }
    }
}
