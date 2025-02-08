﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Domain.DTOs
{
    public class HorarioMedicoDTO
    {
        public int IdMedico { get; set; }

        public int IdHorario { get; set; }

        public int Status { get; set; }

        public DateTime DiaSemana { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraFim { get; set; }
    }
}
