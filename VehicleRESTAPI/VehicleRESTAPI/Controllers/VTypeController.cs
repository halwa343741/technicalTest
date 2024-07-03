using Microsoft.AspNetCore.Mvc;
using VehicleRESTAPI.Attributes;
using VehicleRESTAPI.Repo.Vehicle.Interface;
using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Controllers
{
    [Route("api/vehicle_type")]
    [ApiController]
    public class VTypeController : ControllerBase
    {
        private readonly IvType repo;
        private readonly IvBrand brandRepo;

        public VTypeController(IvType repo, IvBrand brandRepo) {
            this.repo = repo;
            this.brandRepo = brandRepo;
        }

        [JwtAdmin]
        [HttpPut("create")]
        public async Task<IActionResult> Create(VTypeModel dto) {
            try {
                if (dto.brand_id != null && dto.brand_id == 0) dto.brand_id = null;
                if (dto.brand_id != null) {
                    var brand = brandRepo.read().SingleOrDefault(b => b.id.Equals(dto.brand_id));
                    if (brand == null) return BadRequest("Brand id invalid!");
                }

                if (repo.isTypeExists(dto)) return BadRequest("Type already exists!");

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
                if (result == null) return BadRequest("Type not found!");
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
                if (result == null) return BadRequest("Type not found!");
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
        public async Task<IActionResult> Update([FromBody] VTypeModel dto, [FromQuery] int id) {
            try {
                if (dto.brand_id != null && dto.brand_id == 0) dto.brand_id = null;
                if (dto.brand_id != null) {
                    var brand = brandRepo.read().SingleOrDefault(b => b.id.Equals(dto.brand_id));
                    if (brand == null) return BadRequest("Brand id invalid!");
                }

                dto.id = id;
                if (repo.isTypeExists(dto)) return BadRequest("Type already exists!");

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
