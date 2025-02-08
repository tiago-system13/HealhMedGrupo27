using HealthMed.Grupo27.Application.Interfaces;
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

        /// <summary>
        /// Cadastra um novo horário para um médico.
        /// </summary>
        /// <param name="horarioMedico">Objeto contendo as informações do horário a ser cadastrado.</param>
        /// <returns></returns>
        [HttpPost("cadastrar")]
        public async Task<IActionResult> CadastroHorarioMedico([FromBody] HorarioMedico horarioMedico)
        {
            try
            {
                var medicoExiste = await _medicoRepository.GetByIdAsync(horarioMedico.IdMedico);
                if (medicoExiste == null)
                {
                    return BadRequest("Médico não encontrado.");
                }

                await _horarioRepository.AdicionarAsync(horarioMedico);
                return Ok("Horário cadastrado com sucesso.");
            }
            catch (Exception Ex)
            {
                return BadRequest($"Error: {Ex.Message}");
            }   
        }

        /// <summary>
        /// Obtém a lista de todos os horários médicos cadastrados.
        /// </summary>
        /// <returns>Retorna uma lista de horários médicos.</returns>
        [HttpGet]
        public async Task<IActionResult> GetHorarios() => Ok(await _horarioRepository.GetHorariosAsync());
    }

}
