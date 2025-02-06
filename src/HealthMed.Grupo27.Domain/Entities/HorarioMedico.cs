using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Domain.Entities
{
    public class HorarioMedico
    {
        public int IdHorarioMedico { get; set; }

        public int IdMedico { get; set; }

        public DateTime DiaAtendimento { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraFim { get; set; }
    }
}
