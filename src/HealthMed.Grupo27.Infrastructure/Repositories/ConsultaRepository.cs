using HealthMed.Grupo27.Infrastructure.Data;
using HealthMed.Grupo27.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using HealthMed.Grupo27.Domain.Interfaces;

namespace HealthMed.Grupo27.Infrastructure.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly AppDbContext _context;

        public ConsultaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteConsultaNoHorario(int idMedico, DateTime inicio, DateTime fim)
        {
            return await _context.Consultas.AnyAsync(c => c.IdMedico == idMedico &&
                                                          ((inicio >= c.HoraInicio && inicio < c.HoraFim) ||
                                                          (fim > c.HoraInicio && fim <= c.HoraFim)));
        }

        public async Task AdicionarAsync(Consulta consulta)
        {
            await _context.Consultas.AddAsync(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Consulta consulta)
        {
            _context.Consultas.Update(consulta);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Consulta>> GetConsultasAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Consulta> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Consulta consulta)
        {
            throw new NotImplementedException();
        }

        public Task<Consulta> ObterPorIdAsync(int idConsulta)
        {
            throw new NotImplementedException();
        }

        Task<Consulta> IConsultaRepository.GetConsultasAsync()
        {
            throw new NotImplementedException();
        }
    }

}
