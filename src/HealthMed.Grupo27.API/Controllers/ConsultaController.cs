using HealthMed.Grupo27.Application.Interfaces;
using HealthMed.Grupo27.Domain.Entities;
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
            var diaSemana = (int)consulta.DtHoraInicio.DayOfWeek;

            var horarioValido = horariosDisponiveis.Any(h => h.DiaSemana == diaSemana &&
                                                              consulta.DtHoraInicio.TimeOfDay >= h.HoraInicio &&
                                                              consulta.DtHoraFim.TimeOfDay <= h.HoraFim);
            if (!horarioValido)
            {
                return BadRequest("O médico não tem disponibilidade nesse horário.");
            }

            var existeConflito = await _consultaRepository.ExisteConsultaNoHorario(consulta.IdMedico, consulta.DtHoraInicio, consulta.DtHoraFim);
            if (existeConflito)
            {
                return BadRequest("Já existe uma consulta agendada para este horário.");
            }

            await _consultaRepository.AdicionarAsync(consulta);
            return Ok("Consulta cadastrada com sucesso.");
        }
    }
}
