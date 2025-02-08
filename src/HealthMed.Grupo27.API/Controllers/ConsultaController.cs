using HealthMed.Grupo27.Application.Interfaces;
using HealthMed.Grupo27.Domain.DTOs;
using HealthMed.Grupo27.Domain.Entities;
using HealthMed.Grupo27.Domain.Enums;
using HealthMed.Grupo27.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<ConsultaController> _logger;

        public ConsultaController(IConsultaRepository consultaRepository, IHorarioMedicoRepository horarioRepository, IMedicoRepository medicoRepository, IPacienteRepository pacienteRepository, ILogger<ConsultaController> logger)
        {
            _consultaRepository = consultaRepository;
            _horarioRepository = horarioRepository;
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todas as consultas médicas cadastradas.
        /// </summary>
        /// <returns>Retorna todas as consultas médicas cadastradas.</returns>
        [HttpGet]
        public async Task<IActionResult> GetConsultas() => Ok(await _consultaRepository.GetConsultasAsync());

        /// <summary>
        /// Cadastra uma nova consulta médica.
        /// </summary>
        /// <param name="consultaDTO">Dados da consulta a ser cadastrada.</param>
        /// <returns></returns>
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastrarConsulta([FromBody] ConsultaDTO consultaDTO)
        {
            try
            {
                _logger.LogInformation("Iniciando o cadastro da consulta.");
                var horariosDisponiveis = await _horarioRepository.GetHorariosPorMedicoAsync(consultaDTO.IdMedico);

                if (horariosDisponiveis == null)
                {
                    return BadRequest("Não há horários disponíveis para o médico escolhido.");
                }

                var horarioValido = horariosDisponiveis.Any(h => h.DiaSemana.DayOfWeek == consultaDTO.DiaConsulta.DayOfWeek &&
                                                                  consultaDTO.HoraInicio.TimeOfDay >= h.HoraInicio.TimeOfDay &&
                                                                  consultaDTO.HoraFim.TimeOfDay <= h.HoraFim.TimeOfDay);
                if (!horarioValido)
                {
                    return BadRequest("O médico não tem disponibilidade nesse horário.");
                }

                var existeConflito = await _consultaRepository.ExisteConsultaNoHorario(consultaDTO.IdMedico, consultaDTO.HoraInicio, consultaDTO.HoraFim);
                if (existeConflito)
                {
                    return BadRequest("Já existe uma consulta agendada para este horário.");
                }

                Consulta consulta = new Consulta()
                {
                    IdMedico = consultaDTO.IdMedico,
                    IdPaciente = consultaDTO.IdPaciente,
                    HoraInicio = consultaDTO.HoraInicio,
                    HoraFim = consultaDTO.HoraFim,
                    DiaConsulta = consultaDTO.DiaConsulta,
                    StatusConsulta = StatusConsultaType.Cadastrada
                };

                await _consultaRepository.AdicionarAsync(consulta);
                return Ok("Consulta cadastrada com sucesso.");
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex, "Erro ao cadastrar a consulta.");
                return BadRequest($"Error: {Ex.Message}");
            }
        }

        /// <summary>
        /// Confirma ou cancela uma consulta médica.
        /// </summary>
        /// <param name="idConsulta">ID da consulta a ser atualizada.</param>
        /// <param name="status">Novo status da consulta ( Cadastrada = 0, Cancelada = 1, Confirmada = 2)</param>
        /// <returns></returns>
        [HttpPut("confirmar/{idConsulta}")]
        public async Task<IActionResult> ConfirmarConsultaMedica(int idConsulta, [FromBody] StatusConsultaType status)
        {
            try
            {
                _logger.LogInformation($"Iniciando a confirmação da consulta ID: {idConsulta}");

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
            catch (Exception Ex)
            {
                _logger.LogError(Ex, $"Erro ao confirmar a consulta ID: {idConsulta}"); 
                return BadRequest($"Error: {Ex.Message}");
            }   
        }
    }
}
