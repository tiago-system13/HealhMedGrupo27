using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Grupo27.Domain.DTOs
{
    public class UserToken
    {
        public long UserId { get; set; }
        public List<KeyValuePair<string, bool>> Profiles { get; set; }
        public List<string> Pessoas { get; set; }
    }
}
