using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthMed.Grupo27.Domain.Entities;

namespace HealthMed.Grupo27.Application.Interfaces
{
    public interface IConsultaRepository
    {
        Task<bool> ExisteConsultaNoHorario(int idMedico, DateTime inicio, DateTime fim);

        Task<Consulta> ObterPorIdAsync(int idConsulta);

        Task<Consulta> GetConsultasAsync();

        Task AdicionarAsync(Consulta consulta);

        Task AtualizarAsync(Consulta consulta);
    }
}
