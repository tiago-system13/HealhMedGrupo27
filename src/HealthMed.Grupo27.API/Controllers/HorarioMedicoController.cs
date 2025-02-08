using HealthMed.Grupo27.Domain.Entities;
using HealthMed.Grupo27.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Grupo27.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorarioMedicoController : ControllerBase
    {
        private readonly IHorarioMedicoRepository _horarioRepository;
        private readonly IMedicoRepository _medicoRepository;

        public HorarioMedicoController(IHorarioMedicoRepository horarioRepository, IMedicoRepository medicoRepository)
        {
            _horarioRepository = horarioRepository;
            _medicoRepository = medicoRepository;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastroHorarioMedico([FromBody] HorarioMedico horarioMedico)
        {
            var medicoExiste = await _medicoRepository.GetByIdAsync(horarioMedico.IdMedico);
            if (medicoExiste == null)
            {
                return BadRequest("Médico não encontrado.");
            }

            await _horarioRepository.AdicionarAsync(horarioMedico);
            return Ok("Horário cadastrado com sucesso.");
        }

        [HttpGet]
        public async Task<IActionResult> GetHorarios() => Ok(await _horarioRepository.GetHorariosAsync());
    }

}
