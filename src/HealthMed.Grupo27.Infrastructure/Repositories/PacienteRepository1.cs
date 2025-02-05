﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
