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
    public class HorarioMedicoRepository : IHorarioMedicoRepository
    {
        private readonly AppDbContext _context;
        public HorarioMedicoRepository(AppDbContext context) => _context = context;
        public async Task<IEnumerable<HorarioMedico>> GetHorariosAsync() => await _context.HorariosMedicos.ToListAsync();
        public async Task AddAsync(HorarioMedico horario) { _context.HorariosMedicos.Add(horario); await _context.SaveChangesAsync(); }
    }
}
