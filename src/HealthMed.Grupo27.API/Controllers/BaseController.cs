using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace HealthMed.Grupo27.API.Controllers
{   
    public class BaseController<T> : ControllerBase
    {
        protected IIdentity LoggedInUser => User.Identity;

        protected List<string> UserProfiles => User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

        protected List<string> UserAreas => User.FindAll("Pessoas").Select(c => c.Value).ToList();

        protected bool HasProfile(string profile) => UserProfiles != null && UserProfiles.Any((p) => p == profile);

        protected string LoggedInUserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        protected readonly IHttpContextAccessor accessor;
        protected readonly ILogger<T> logger;

        public BaseController(IHttpContextAccessor accessor, ILogger<T> logger)
        {
            this.accessor = accessor;
            this.logger = logger;
        }
    }
}
