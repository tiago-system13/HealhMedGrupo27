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

        public string Senha { get; set; }

        public List<Medico> Medicos { get; set; }

        public List<Paciente> Pacientes { get; set; }
    }
}
