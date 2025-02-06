using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Domain.Entities
{
    public class Medico : Usuario
    {
        public int IdMedico { get; set; }

        public int IdUsuario { get; set; }

        public string CRM { get; set; }

        public int ValorConsulta { get; set; }

        public string Especialidade { get; set; }
    }
}
