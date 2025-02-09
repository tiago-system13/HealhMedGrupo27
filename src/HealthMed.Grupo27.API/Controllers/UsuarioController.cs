using HealthMed.Grupo27.API.Security;
using HealthMed.Grupo27.Application.Utils;
using HealthMed.Grupo27.Application.ViewModels;
using HealthMed.Grupo27.Domain.DTOs;
using HealthMed.Grupo27.Domain.Entities;
using HealthMed.Grupo27.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthMed.Grupo27.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : BaseController<UserTokenViewModel>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserTokenViewModel> _logger;

        public UsuarioController(IUsuarioRepository usuarioRepository, IConfiguration configuration, IHttpContextAccessor accessor, ILogger<UserTokenViewModel> logger)
            :base(accessor, logger)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;            
        }

        /// <summary>
        /// Realiza o login de um médico no sistema.
        /// </summary>
        /// <param name="request">Objeto contendo as credenciais do médico.</param>
        /// <returns></returns>
        [HttpPost("login-medico")]
        public async Task<IActionResult> LoginMedico([FromBody] LoginMedicoRequest request)
        {
            try
            {
                _logger.LogInformation($"Iniciando o login do medico CRM: {request.CRM}.");
                var senhaCriptografada = HashSenha(request.Senha);
                var usuario = await _usuarioRepository.ObterPorCrmESenhaAsync(request.CRM, senhaCriptografada);

                if (usuario == null)
                {
                    return Unauthorized("Login falhou. Verifique suas credenciais.");
                }

                var token = GerarToken(usuario);
                return Ok(new { Token = token });
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex, $"Erro no login do medico CRM: {request.CRM}");
                return BadRequest($"Error: {Ex.Message}");
            }       
        }

        /// <summary>
        /// Realiza o login de um paciente no sistema.
        /// </summary>
        /// <param name="request">Objeto contendo as credenciais do paciente.</param>
        [HttpPost("login-paciente")]
        public async Task<IActionResult> LoginPaciente([FromBody] LoginPacienteRequest request)
        {
            try
            {
                _logger.LogInformation($"Iniciando o login do paciente Login: {request.Login}.");
                var senhaCriptografada = Utilidades.CriptografarSenha(request.Senha);
                var usuario = await _usuarioRepository.ObterPorLoginSenhaAsync(request.Login, senhaCriptografada);

                if (usuario == null)
                {
                    return Unauthorized("Login falhou. Verifique as credenciais.");
                }

                var token = GerarToken(usuario);
                return Ok(new { Token = token });
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex, $"Erro no login do paciente Login: {request.Login}");
                return BadRequest($"Error: {Ex.Message}");
            }          
        }

        [HttpGet]
        [AuthorizeProfiles(UserProfile.Medico, UserProfile.Administrador, UserProfile.Paciente)]
        public ActionResult<UserTokenViewModel> Get()
        {
            _logger.LogInformation("Get User Token: UserController");
            return new UserTokenViewModel
            {
                UserId = LoggedInUser.Name,
                Profiles = UserProfiles,
                Areas = UserAreas
            };

        }

        private string HashSenha(string senha)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, usuario.IdUsuario.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
