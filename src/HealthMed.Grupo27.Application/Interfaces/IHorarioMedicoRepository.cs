using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthMed.Grupo27.Domain.Entities;

namespace HealthMed.Grupo27.Application.Interfaces
{
    public interface IHorarioMedicoRepository
    {
        Task<IEnumerable<HorarioMedico>> GetHorariosAsync();
        Task AddAsync(HorarioMedico horario);

        Task AdicionarAsync(HorarioMedico horario);

        Task<HorarioMedico> GetHorariosPorMedicoAsync(int IdMedico);
    }


}
