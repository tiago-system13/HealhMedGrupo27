using HealthMed.Grupo27.Domain.Entities;
using HealthMed.Grupo27.Domain.Interfaces;
using HealthMed.Grupo27.Infrastructure.Data;

namespace HealthMed.Grupo27.Infrastructure.Repositories
{
    public class HorarioMedicoRepository : IHorarioMedicoRepository
    {
        private readonly AppDbContext _context;

        public HorarioMedicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(HorarioMedico horario)
        {
            throw new NotImplementedException();
        }

        public async Task AdicionarAsync(HorarioMedico horario)
        {
            await _context.HorariosMedicos.AddAsync(horario);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<HorarioMedico>> GetHorariosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<HorarioMedico>> GetHorariosPorMedicoAsync(int IdMedico)
        {
            return await _context.HorariosMedicos.Where(h => h.IdMedico == IdMedico).ToListAsync();
        }
    }
}
