using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Application.Interfaces
{
    public interface IMedicoRepository
    {
        Task<IEnumerable<Medico>> GetMedicosAsync();
        Task<Medico> GetByIdAsync(int id);
        Task AddAsync(Medico medico);
    }
}
