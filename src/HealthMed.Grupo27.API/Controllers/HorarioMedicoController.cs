using HealthMed.Grupo27.API.Security;
using HealthMed.Grupo27.Domain.DTOs;
using HealthMed.Grupo27.Domain.Entities;
using HealthMed.Grupo27.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Grupo27.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorarioMedicoController : BaseController<ConsultaController>
    {
        private readonly IHorarioMedicoRepository _horarioRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly ILogger<ConsultaController> _logger;

        public HorarioMedicoController
        (
            IHorarioMedicoRepository horarioRepository,
            IMedicoRepository medicoRepository,
            ILogger<ConsultaController> logger,
            IHttpContextAccessor accessor
        )
        :base(accessor, logger)
        {
            _horarioRepository = horarioRepository;
            _medicoRepository = medicoRepository;            
        }

        /// <summary>
        /// Cadastra um novo horário para um médico.
        /// </summary>
        /// <param name="horarioMedico">Objeto contendo as informações do horário a ser cadastrado.</param>
        /// <returns></returns>
        [HttpPost("cadastrar")]
        [AuthorizeProfiles(UserProfile.Medico,UserProfile.Administrador)]
        public async Task<IActionResult> CadastroHorarioMedico([FromBody] HorarioMedicoDTO horarioMedicoDTO)
        {
            try
            {
                _logger.LogInformation($"Iniciando o cadastro de Horario Medico, IdMedico: {horarioMedicoDTO.IdMedico}.");
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
            catch (Exception Ex)
            {
                _logger.LogError(Ex, $"Erro no cadastro de Horario Medico, IdMedico: {horarioMedicoDTO.IdMedico}.");
                return BadRequest($"Error: {Ex.Message}");
            }   
        }

        /// <summary>
        /// Obtém a lista de todos os horários médicos cadastrados.
        /// </summary>
        /// <returns>Retorna uma lista de horários médicos.</returns>
        [HttpGet]
        [AuthorizeProfiles(UserProfile.Medico, UserProfile.Administrador)]
        public async Task<IActionResult> GetHorarios() 
        {
            try
            {
                _logger.LogInformation("Iniciando a busca de horários.");
                return Ok(await _horarioRepository.GetHorariosAsync());
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex, "Erro na busca de horários.");
                return BadRequest($"Error: {Ex.Message}");
            }
        }
            
    }

}
