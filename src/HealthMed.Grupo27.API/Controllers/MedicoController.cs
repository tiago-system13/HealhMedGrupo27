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
        public async Task<IActionResult> GetMedicos() => Ok(await _repository.GetMedicosAsync());
    }
}
