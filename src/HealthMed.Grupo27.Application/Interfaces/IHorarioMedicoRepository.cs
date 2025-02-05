using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Application.Interfaces
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
