using HealthMed.Grupo27.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Domain.DTOs
{
    public class ConsultaDTO
    {
        public int IdPaciente { get; set; }

        public required int IdMedico { get; set; }

        public DateTime DiaConsulta { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraFim { get; set; }
    }
}
