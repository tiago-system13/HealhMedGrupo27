using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Domain.Entities
{
    public class Paciente : Usuario
    {
        public int IdPaciente { get; set; }

        public int IdUsuario { get; set; }

        public string Nome { get; set; }

        public int Telefone { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }
    }
}
