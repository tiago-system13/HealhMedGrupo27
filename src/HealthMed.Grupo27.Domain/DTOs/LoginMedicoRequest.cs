using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Domain.DTOs
{
    public class LoginMedicoRequest
    {
        public string CRM { get; set; }
        public string Senha { get; set; }
    }
}
