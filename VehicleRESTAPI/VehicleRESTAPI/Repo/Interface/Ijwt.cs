using Microsoft.IdentityModel.Tokens;
using VehicleRESTAPI.Models;

namespace VehicleRESTAPI.Repo.Interface
{
    public interface Ijwt
    {
        string? createToken(UsersModel user);
        int? getUserIdFromToken(string token);
        TokenValidationParameters tokenValidationParameters();
    }
}