using HealthMed.Grupo27.Domain.Entities;

namespace HealthMed.Grupo27.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObterPorCrmESenhaAsync(string crm, string senhaCriptografada);

        Task<Usuario> ObterPorLoginSenhaAsync(string login, string senhaCriptografada);
    }

}
