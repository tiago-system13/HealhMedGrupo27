using HealthMed.Grupo27.Application.Interfaces;
using HealthMed.Grupo27.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using HealthMed.Grupo27.Infrastructure.Data;
using HealthMed.Grupo27.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ObterPorCrmESenhaAsync(string crm, string senhaCriptografada)
        {
            return await _context.Usuarios
                .Include(u => u.Medicos)
                .FirstOrDefaultAsync(u => u.Medicos.Any(m => m.CRM == crm) && u.Senha == senhaCriptografada);
        }

        public async Task<Usuario> ObterPorLoginSenhaAsync(string login, string senhaCriptografada)
        {
            return await _context.Usuarios
                .Include(u => u.Pacientes)
                .FirstOrDefaultAsync(u => u.Senha == senhaCriptografada &&
                    (u.Pacientes.Any(p => p.Email == login || p.CPF == login)));
        }
    }
}
