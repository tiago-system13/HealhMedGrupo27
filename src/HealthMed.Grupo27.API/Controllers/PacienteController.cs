using HealthMed.Grupo27.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Grupo27.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteRepository _repository;
        public PacienteController(IPacienteRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> GetPacientes() => Ok(await _repository.GetPacientesAsync());
    }
}
