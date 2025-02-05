using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthMed.Grupo27.Application.Interfaces;
using HealthMed.Grupo27.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthMed.Grupo27.Infrastructure.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly AppDbContext _context;
        public ConsultaRepository(AppDbContext context) => _context = context;
        public async Task<IEnumerable<Consulta>> GetConsultasAsync() => await _context.Consultas.ToListAsync();
        public async Task<Consulta> GetByIdAsync(int id) => await _context.Consultas.FindAsync(id);
        public async Task AddAsync(Consulta consulta) { _context.Consultas.Add(consulta); await _context.SaveChangesAsync(); }
    }

}
