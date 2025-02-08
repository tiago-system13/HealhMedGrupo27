using HealthMed.Grupo27.Domain.Entities;

namespace HealthMed.Grupo27.Domain.Interfaces
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
