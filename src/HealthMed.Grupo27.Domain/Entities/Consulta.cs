using HealthMed.Grupo27.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Domain.Entities
{
    public class Consulta
    {
        public int IdConsulta { get; set; }

        public int IdPaciente { get; set; }

        public int IdMedico { get; set; }

        public DateTime DiaConsulta { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraFim { get; set; }

        public StatusConsultaType StatusConsulta { get; set; }

        public Medico Medico { get; set; }

        public Paciente Paciente { get; set; }
    }
}
