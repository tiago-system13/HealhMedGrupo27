
using Microsoft.AspNetCore.Authorization;

namespace HealthMed.Grupo27.API.Security
{
    public class AuthorizeProfilesAttribute : AuthorizeAttribute
    {
        public AuthorizeProfilesAttribute()
        {
            base.Roles = UserProfile.Medico;
        }
        public AuthorizeProfilesAttribute(params string[] roles) : this()
        {
            if (roles.Length > 0)
            {
                base.Roles += "," + string.Join(',', roles);
            }
        }
    }
}
