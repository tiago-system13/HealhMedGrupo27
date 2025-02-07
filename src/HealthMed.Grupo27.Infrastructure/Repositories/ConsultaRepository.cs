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
    }

}
