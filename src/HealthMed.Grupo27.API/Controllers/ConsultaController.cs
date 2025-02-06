using HealthMed.Grupo27.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Grupo27.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaRepository _repository;
        public ConsultaController(IConsultaRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> GetConsultas() => Ok(await _repository.GetConsultasAsync());
    }
}
