using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Domain.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nome { get; set; } 

        public string Email { get; set; }

        public string CPF { get; set; }

        public string Telefone { get; set; }

        public string Senha { get; set; }

        public DateTime DataNascimento { get; set; }
    }
}
