using Moq;
using HealthMed.Grupo27.Infrastructure.Data;
using HealthMed.Grupo27.Infrastructure.Repositories;
using HealthMed.Grupo27.Domain.Entities;
using HealthMed.Grupo27.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Grupo27.Tests
{
    public class RepositoryTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly Mock<DbSet<Consulta>> _mockSet;
        private readonly ConsultaRepository _consultaRepository;
        private readonly List<Consulta> _dados;

        private readonly MedicoRepository _medicoRepository;
        private readonly PacienteRepository _pacienteRepository;
        private readonly Mock<DbSet<Medico>> _mockMedicos;

        public RepositoryTests()
        {
            _mockContext = new Mock<AppDbContext>();
            _dados = new List<Consulta>();  // Lista simulada no banco

            //_mockSet = MockDbSetExtensions.CriarDbSetMock(_dados);

            _mockContext.Setup(m => m.Consultas).Returns(_mockSet.Object);
            _consultaRepository = new ConsultaRepository(_mockContext.Object);

            //ooo
            //_mockContext = new Mock<AppDbContext>();

            //var medicos = new List<Medico>
            //{
            //    new Medico { IdMedico = 1, Nome = "Dr. Teste", CRM = "123456", Especialidade = "Cardiologia" }
            //};

            //_mockMedicos = CriarDbSetMock(medicos);
            //_mockContext.Setup(x => x.Medicos).Returns(_mockMedicos.Object);

            //_medicoRepository = new MedicoRepository(_mockContext.Object);

            //_mockContext = new Mock<AppDbContext>();
            //_consultaRepository = new ConsultaRepository(_mockContext.Object);
            //_medicoRepository = new MedicoRepository(_mockContext.Object);
            //_pacienteRepository = new PacienteRepository(_mockContext.Object);
        }

        [Fact]
        public async Task AdicionarConsulta_DeveSalvarNoBanco()
        {
            var consulta = new Consulta { IdMedico = 1, IdPaciente = 1, HoraInicio = DateTime.Now, HoraFim = DateTime.Now.AddHours(1), StatusConsulta = StatusConsultaType.Confirmada };

            await _consultaRepository.AdicionarAsync(consulta);

            Assert.Contains(consulta, _dados);
            _mockSet.Verify(x => x.AddAsync(consulta, It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        //[Fact]
        //public async Task AdicionarConsulta_DeveSalvarNoBanco()
        //{
        //    var consulta = new Consulta { IdMedico = 1, IdPaciente = 1, HoraInicio = DateTime.Now, HoraFim = DateTime.Now.AddHours(1), StatusConsulta = StatusConsultaType.Confirmada };
        //    await _consultaRepository.AdicionarAsync(consulta);
        //    _mockContext.Verify(x => x.Consultas.AddAsync(consulta, default), Times.Once);
        //    _mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        //}

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarMedicoCorreto()
        {
            var medico = new Medico { IdMedico = 1, Nome = "Dr. Teste", CRM = "123456", Especialidade = "Cardiologia" };
            _mockContext.Setup(x => x.Medicos.FindAsync(1)).ReturnsAsync(medico);

            var resultado = await _medicoRepository.GetByIdAsync(1);
            Assert.NotNull(resultado);
            Assert.Equal("Dr. Teste", resultado.Nome);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarPacienteCorreto()
        {
            var paciente = new Paciente { IdPaciente = 1, Nome = "Paciente Teste", Email = "teste@email.com", CPF = "12345678900" };
            _mockContext.Setup(x => x.Pacientes.FindAsync(1)).ReturnsAsync(paciente);

            var resultado = await _pacienteRepository.GetByIdAsync(1);
            Assert.NotNull(resultado);
            Assert.Equal("Paciente Teste", resultado.Nome);
        }
    }
}