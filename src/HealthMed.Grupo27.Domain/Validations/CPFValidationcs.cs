using System.ComponentModel.DataAnnotations;

namespace HealthMed.Grupo27.Domain.Validations
{
    public class CPFAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;

            string cpf = value.ToString().Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            // Implemente a lógica de validação do CPF aqui
            // Exemplo básico (não inclui validação completa dos dígitos verificadores):
            return true; // Substitua pela lógica real
        }
    }
}
 