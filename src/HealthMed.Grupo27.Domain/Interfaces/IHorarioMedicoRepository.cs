using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthMed.Grupo27.Domain.Entities;

namespace HealthMed.Grupo27.Domain.Interfaces
{
    public interface IHorarioMedicoRepository
    {
        Task<IEnumerable<HorarioMedico>> GetHorariosAsync();
        Task AddAsync(HorarioMedico horario);

        Task AdicionarAsync(HorarioMedico horario);

        Task<List<HorarioMedico>> GetHorariosPorMedicoAsync(int IdMedico);
    }


}
