using HealthMed.Grupo27.Application.Interfaces;
using HealthMed.Grupo27.Domain.DTOs;
using HealthMed.Grupo27.Domain.Entities;
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
        public async Task<IActionResult> CadastroHorarioMedico([FromBody] HorarioMedicoDTO horarioMedicoDTO)
        {
            var medicoExiste = await _medicoRepository.GetByIdAsync(horarioMedicoDTO.IdMedico);
            if (medicoExiste == null)
            {
                return BadRequest("Médico não encontrado.");
            }

            HorarioMedico horarioMedico = new HorarioMedico()
            {
                IdMedico = horarioMedicoDTO.IdMedico,
                Status = 1,
                DiaSemana = horarioMedicoDTO.DiaSemana,
                HoraInicio = horarioMedicoDTO.HoraInicio,
                HoraFim = horarioMedicoDTO.HoraFim
            };

            await _horarioRepository.AdicionarAsync(horarioMedico);
            return Ok("Horário cadastrado com sucesso.");
        }

        [HttpGet]
        public async Task<IActionResult> GetHorarios() => Ok(await _horarioRepository.GetHorariosAsync());
    }

}
