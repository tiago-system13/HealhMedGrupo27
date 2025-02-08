using HealthMed.Grupo27.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using HealthMed.Grupo27.Infrastructure.Data;
using HealthMed.Grupo27.Domain.Interfaces;

namespace HealthMed.Grupo27.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly AppDbContext _context;
        public PacienteRepository(AppDbContext context) => _context = context;
        public async Task<IEnumerable<Paciente>> GetPacientesAsync() => await _context.Pacientes.ToListAsync();
        public async Task<Paciente> GetByIdAsync(int id) => await _context.Pacientes.FindAsync(id);
        public async Task AddAsync(Paciente paciente) { _context.Pacientes.Add(paciente); await _context.SaveChangesAsync(); }
    }
}
