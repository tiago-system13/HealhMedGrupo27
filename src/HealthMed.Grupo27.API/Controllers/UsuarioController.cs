using HealthMed.Grupo27.Application.Interfaces;
using HealthMed.Grupo27.Application.Utils;
using HealthMed.Grupo27.Domain.DTOs;
using HealthMed.Grupo27.Domain.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthMed.Grupo27.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public UsuarioController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        [HttpPost("login-medico")]
        public async Task<IActionResult> LoginMedico([FromBody] LoginMedicoRequest request)
        {
            var senhaCriptografada = HashSenha(request.Senha);
            var usuario = await _usuarioRepository.ObterPorCrmESenhaAsync(request.CRM, senhaCriptografada);

            if (usuario == null)
            {
                return Unauthorized("Login falhou. Verifique suas credenciais.");
            }

            var token = GerarToken(usuario);
            return Ok(new { Token = token });
        }

        [HttpPost("login-paciente")]
        public async Task<IActionResult> LoginPaciente([FromBody] LoginPacienteRequest request)
        {
            var senhaCriptografada = Utilidades.CriptografarSenha(request.Senha);
            var usuario = await _usuarioRepository.ObterPorLoginSenhaAsync(request.Login, senhaCriptografada);

            if (usuario == null)
            {
                return Unauthorized("Login falhou. Verifique as credenciais.");
            }

            var token = GerarToken(usuario);
            return Ok(new { Token = token });
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
