using EABR.QualityService.Apis.Core.Criptography;
using HealthMed.Grupo27.Application.Utils;
using HealthMed.Grupo27.Domain.DTOs;
using HealthMed.Grupo27.Domain.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace HealthMed.Grupo27.API.Security
{

    public class AuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        private readonly ILogger<AuthenticationHandler> logger;

        public AuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory loggerFactory, UrlEncoder encoder,
            ISystemClock clock, ILogger<AuthenticationHandler> logger) : base(options, loggerFactory, encoder, clock)
        {
            this.logger = logger;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                logger.LogError("Missing authorization header. Returning 401");
                return Task.FromResult(AuthenticateResult.Fail("Missing authorization header"));
            }

            UserToken user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

                if ("Bearer" != authHeader.Scheme)
                {
                    logger.LogError("Authorization header scheme mismatch. Returning 401");
                    return Task.FromResult(AuthenticateResult.Fail("Authorization header scheme mismatch"));
                }

                if (string.IsNullOrEmpty(authHeader.Parameter))
                {
                    logger.LogError("Authorization header parameter not found. Returning 401");
                    return Task.FromResult(AuthenticateResult.Fail("Authorization header parameter not found"));
                }

                logger.LogDebug($"Decrypt token {Utilidades.Redact(authHeader.Parameter)}");

                var decrypt = HealthMedCrypto.Decrypt(authHeader.Parameter);
                user = JsonConvert.DeserializeObject<UserToken>(decrypt);

            }
            catch (AuthorizationException exception)
            {
                logger.LogError(exception, $"Error parsing authorization header. Returning 401: {exception.Message}");
                return Task.FromResult(AuthenticateResult.Fail("Error parsing authorization header"));
            }

            if (user == null)
            {
                logger.LogError("User not found. Returning 401");
                return Task.FromResult(AuthenticateResult.Fail("User not found"));
            }

            List<Claim> claims = new List<Claim>();

            string userId = user.UserId.ToString();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
            claims.Add(new Claim(ClaimTypes.Name, userId));

            if (user.Profiles != null && user.Profiles.Count > 0)
            {
                foreach (var profile in user.Profiles)
                {
                    if (profile.Value)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, profile.Key));
                    }
                }
            }

            if (user.Pessoas != null && user.Pessoas.Count > 0)
            {
                foreach (var pessoa in user.Pessoas)
                {
                    if (!string.IsNullOrEmpty(pessoa))
                    {
                        claims.Add(new Claim("Pessoas", pessoa));
                    }
                }
            }

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));

        }
    }

}