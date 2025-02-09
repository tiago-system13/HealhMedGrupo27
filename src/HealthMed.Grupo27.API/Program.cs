//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using HealthMed.Grupo27.Infrastructure.Data;
using HealthMed.Grupo27.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using HealthMed.Grupo27.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

var srv = Environment.GetEnvironmentVariable("SRV_BD");
var usr = Environment.GetEnvironmentVariable("USR_BD");
var pwd = Environment.GetEnvironmentVariable("PWD_BD");

var connectionString = configuration.GetConnectionString("DefaultConnection");
connectionString = string.Format(connectionString, srv, usr, pwd);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IHorarioMedicoRepository, HorarioMedicoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthMed API", Version = "v1" });   
    c.AddSecurityDefinition("Bearer", new ApiKeyScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = "header",
        Type = "apiKey"
    });    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthMed API v1"));
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();