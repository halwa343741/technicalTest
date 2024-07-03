using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using VehicleRESTAPI.Repo.Interface;
using VehicleRESTAPI.Repo;

namespace VehicleRESTAPI.Attributes.Filter
{
    public class JwtFilter:IAuthorizationFilter
    {
        private readonly IUsers userRepo;
        private readonly Ijwt jwtRepo;

        public JwtFilter(IUsers userRepo, Ijwt jwtRepo) {
            this.userRepo = userRepo;
            this.jwtRepo = jwtRepo;
        }

        public void OnAuthorization(AuthorizationFilterContext context) {
            var token = context.HttpContext.getToken();
            if (token.isNullOrEmpty()) {
                context.Result = new UnauthorizedResult();
                return;
            }
            var userId = jwtRepo.getUserIdFromToken(token);
            if (userId == null) {
                context.Result = new UnauthorizedResult();
                return;
            }
            var user = userRepo.read().SingleOrDefault(u => u.id.Equals(userId));
            if (user == null) {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
