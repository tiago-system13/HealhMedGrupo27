using HealthMed.Grupo27.Domain.Entities;

namespace HealthMed.Grupo27.Domain.Interfaces
{
    public interface IMedicoRepository
    {
        Task<IEnumerable<Medico>> GetMedicosAsync();
        Task<Medico> GetByIdAsync(int id);
        Task AddAsync(Medico medico);
    }
}
