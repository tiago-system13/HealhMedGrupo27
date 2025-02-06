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
    }
}
