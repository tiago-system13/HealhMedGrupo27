using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Application.Interfaces
{
    public interface IConsultaRepository
    {
        Task<IEnumerable<Consulta>> GetConsultasAsync();
        Task<Consulta> GetByIdAsync(int id);
        Task AddAsync(Consulta consulta);
    }
}
