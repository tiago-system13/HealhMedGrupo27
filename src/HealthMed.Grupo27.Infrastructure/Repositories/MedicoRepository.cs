using HealthMed.Grupo27.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using HealthMed.Grupo27.Infrastructure.Data;
using HealthMed.Grupo27.Domain.Interfaces;

namespace HealthMed.Grupo27.Infrastructure.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly AppDbContext _context;
        public MedicoRepository(AppDbContext context) => _context = context;
        public async Task<IEnumerable<Medico>> GetMedicosAsync() => await _context.Medicos.ToListAsync();
        public async Task<Medico> GetByIdAsync(int id) => await _context.Medicos.FindAsync(id);
        public async Task AddAsync(Medico medico) { _context.Medicos.Add(medico); await _context.SaveChangesAsync(); }
    }
}
