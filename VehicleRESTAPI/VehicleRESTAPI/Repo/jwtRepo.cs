using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehicleRESTAPI.Models;
using VehicleRESTAPI.Repo.Interface;

namespace VehicleRESTAPI.Repo
{
    public class jwtRepo : Ijwt
    {
        private readonly string jwtKey;

        public jwtRepo(IConfiguration config) {
            this.jwtKey = config["JWT:Key"];
        }

        public string? createToken(UsersModel user) {
            try {
                List<Claim> claims = new List<Claim>{
                    new Claim("id",user.id.ToString().Encrypt()!),
                    new Claim("name",user.name)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: cred);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch {
                return null;
            }
        }

        public TokenValidationParameters tokenValidationParameters() {
            return new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }

        public int? getUserIdFromToken(string token) {
            try {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters(), out var detailToken);
                var claim = principal.FindFirst("id");
                if (claim == null) return null;
                var userId = claim.Value;
                if (userId.isNullOrEmpty() || !int.TryParse(userId.Decrypt(), out var id)) return null;
                return id;
            }
            catch {
                return null;
            }
        }

    }
}
