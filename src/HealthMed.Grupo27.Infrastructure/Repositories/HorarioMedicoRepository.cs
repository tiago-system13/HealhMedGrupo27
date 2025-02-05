using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Infrastructure.Repositories
{
    public class HorarioMedicoRepository : IHorarioMedicoRepository
    {
        private readonly AppDbContext _context;
        public HorarioMedicoRepository(AppDbContext context) => _context = context;
        public async Task<IEnumerable<HorarioMedico>> GetHorariosAsync() => await _context.HorariosMedicos.ToListAsync();
        public async Task AddAsync(HorarioMedico horario) { _context.HorariosMedicos.Add(horario); await _context.SaveChangesAsync(); }
    }
}
