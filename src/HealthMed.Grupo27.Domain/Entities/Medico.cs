using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Domain.Entities
{
    public class Medico
    {
        public int IdMedico { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        public string CRM { get; set; }

        public decimal ValorConsulta { get; set; }

        public string Especialidade { get; set; }

        public List<Usuario> Usuarios { get; set; }
    }
}
