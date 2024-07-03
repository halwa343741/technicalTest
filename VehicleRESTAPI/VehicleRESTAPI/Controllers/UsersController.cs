using Microsoft.AspNetCore.Mvc;
using VehicleRESTAPI.Attributes;
using VehicleRESTAPI.Models;
using VehicleRESTAPI.Repo;
using VehicleRESTAPI.Repo.Interface;

namespace VehicleRESTAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers repo;
        private readonly Ijwt jwtRepo;

        public UsersController(IUsers repo, Ijwt jwtRepo) {
            this.repo = repo;
            this.jwtRepo = jwtRepo;
        }

        [HttpPut("create")]
        public async Task<IActionResult> Create([FromBody] UsersModel dto) {
            try {
                if (repo.isUsersExists(dto)) return BadRequest("User already exists!");

                var result = await repo.create(dto);
                if (result == null) return BadRequest("Create failed!");

                return Ok(result);
            }
            catch {
                return BadRequest("Something went wrong");
            }
        }

        [Jwt]
        [HttpGet("read")]
        public IActionResult Get(int id) {
            try {
                var result = repo.read().SingleOrDefault(d => d.id.Equals(id));
                if (result == null) return BadRequest("User not found!");
                return Ok(result);
            }
            catch {
                return BadRequest("Something went wrong");
            }
        }

        [Jwt]
        [HttpGet("reads")]
        public IActionResult Gets() {
            try {
                var result = repo.read().ToList();
                if (result == null) return BadRequest("User not found!");
                return Ok(result);
            }
            catch {
                return BadRequest("Something went wrong");
            }
        }

        [JwtAdmin]
        [HttpPatch("update")]
        public async Task<IActionResult> Update([FromBody] UsersModel dto, [FromQuery] int id) {
            try {
                dto.id = id;
                if (repo.isUsersExists(dto)) return BadRequest("User already exists!");

                var result = await repo.update(dto);
                if (result == null) return BadRequest("Update failed");

                return Ok(result);
            }
            catch {
                return BadRequest("Something went wrong");
            }
        }

        [JwtAdmin]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id) {
            try {
                if (await repo.delete(id)) return Ok("Data Deleted");
                return BadRequest("Delete failed!");
            }
            catch {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("login")]
        public IActionResult Login(string name) {
            try {
                var user = repo.read().SingleOrDefault(u => u.name.ToLower().Equals(name.ToLower()));
                if (user == null) return BadRequest("User not found!");
                var token = jwtRepo.createToken(user);
                if (token.isNullOrEmpty()) return BadRequest("Token creation failed!");    
                return Ok(token);
            }
            catch {
                return BadRequest("Something went wrong");
            }
        }
    }
}
