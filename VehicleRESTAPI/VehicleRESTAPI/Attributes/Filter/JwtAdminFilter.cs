using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VehicleRESTAPI.Repo;
using VehicleRESTAPI.Repo.Interface;

namespace VehicleRESTAPI.Attributes.Filter
{
    public class JwtAdminFilter : IAuthorizationFilter
    {
        private readonly IUsers userRepo;
        private readonly Ijwt jwtRepo;

        public JwtAdminFilter(IUsers userRepo, Ijwt jwtRepo)
        {
            this.userRepo = userRepo;
            this.jwtRepo = jwtRepo;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.getToken();
            if (token.isNullOrEmpty())
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var userId = jwtRepo.getUserIdFromToken(token);
            if (userId == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var user = userRepo.read().SingleOrDefault(u => u.id.Equals(userId) && u.is_admin);
            if (user == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
