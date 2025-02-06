using HealthMed.Grupo27.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Grupo27.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorarioMedicoController : ControllerBase
    {
        private readonly IHorarioMedicoRepository _repository;
        public HorarioMedicoController(IHorarioMedicoRepository repository) => _repository = repository;

        [HttpGet]
        public async Task<IActionResult> GetHorarios() => Ok(await _repository.GetHorariosAsync());
    }

}
