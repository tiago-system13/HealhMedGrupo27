using HealthMed.Grupo27.Application.Interfaces;
using HealthMed.Grupo27.Domain.Entities;
using HealthMed.Grupo27.Domain.Enums;
using HealthMed.Grupo27.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Grupo27.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IHorarioMedicoRepository _horarioRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;

        public ConsultaController(IConsultaRepository consultaRepository, IHorarioMedicoRepository horarioRepository, IMedicoRepository medicoRepository, IPacienteRepository pacienteRepository)
        {
            _consultaRepository = consultaRepository;
            _horarioRepository = horarioRepository;
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsultas() => Ok(await _consultaRepository.GetConsultasAsync());

        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarConsulta([FromBody] Consulta consulta)
        {
            var horariosDisponiveis = await _horarioRepository.GetHorariosPorMedicoAsync(consulta.IdMedico);
            var diaSemana = consulta.DiaConsulta;

            var horarioValido = horariosDisponiveis.Any(h => h.DiaSemana == diaSemana &&
                                                              consulta.HoraInicio.TimeOfDay >= h.HoraInicio &&
                                                              consulta.HoraFim.TimeOfDay <= h.HoraFim);
            if (!horarioValido)
            {
                return BadRequest("O médico não tem disponibilidade nesse horário.");
            }

            var existeConflito = await _consultaRepository.ExisteConsultaNoHorario(consulta.IdMedico, consulta.HoraInicio, consulta.HoraFim);
            if (existeConflito)
            {
                return BadRequest("Já existe uma consulta agendada para este horário.");
            }

            await _consultaRepository.AdicionarAsync(consulta);
            return Ok("Consulta cadastrada com sucesso.");
        }

        [HttpPut("confirmar/{idConsulta}")]
        public async Task<IActionResult> ConfirmarConsultaMedica(int idConsulta, [FromBody] StatusConsultaType status)
        {
            var consulta = await _consultaRepository.ObterPorIdAsync(idConsulta);
            if (consulta == null)
            {
                return NotFound("Consulta não encontrada.");
            }

            if (status != StatusConsultaType.Cancelada && status != StatusConsultaType.Confirmada)
            {
                return BadRequest("Status inválido. Use 1 para cancelar e 2 para confirmar.");
            }

            consulta.StatusConsulta = status;
            await _consultaRepository.AtualizarAsync(consulta);

            return Ok("Consulta atualizada com sucesso.");
        }
    }
}
