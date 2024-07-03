using Microsoft.AspNetCore.Mvc;
using VehicleRESTAPI.Attributes;
using VehicleRESTAPI.Repo.Vehicle.Interface;
using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Controllers
{
    [Route("api/vehicle_year")]
    [ApiController]
    public class VYearController : ControllerBase
    {
        private readonly IvYear repo;

        public VYearController(IvYear repo) {
            this.repo = repo;
        }

        [JwtAdmin]
        [HttpPut("create")]
        public async Task<IActionResult> Create(VYearModel dto) {
            try {
                if (dto.year < 1) return BadRequest("Year must be a positive number");

                if (repo.isYearExists(dto)) return BadRequest("Year already exists!");

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
        public IActionResult Read(int id) {
            try {
                var result = repo.read().SingleOrDefault(b => b.id.Equals(id));
                if (result == null) return BadRequest("Year not found!");
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
                if (result == null) return BadRequest("Year not found!");
                return Ok(result);
            }
            catch {
                return BadRequest("Something went wrong");
            }
        }

        [Jwt]
        [HttpGet("pagination")]
        public IActionResult ReadPagination(int page = 1, int pageSize = 10) {
            try {
                var query = repo.read();
                var totalData = query.Count();
                var totalPages = (int)Math.Ceiling(totalData / (double)pageSize);

                var paginatedResult = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                var response = new {
                    TotalData = totalData,
                    TotalPages = totalPages,
                    Page = page,
                    PageSize = pageSize,
                    Data = paginatedResult
                };

                return Ok(response);
            }
            catch {
                return BadRequest("Something went wrong");
            }
        }

        [JwtAdmin]
        [HttpPatch("update")]
        public async Task<IActionResult> Update([FromBody] VYearModel dto, [FromQuery] int id) {
            try {
                if (dto.year < 1) return BadRequest("Year must be a positive number");

                dto.id = id;
                if (repo.isYearExists(dto)) return BadRequest("Year already exists!");

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
    }
}
