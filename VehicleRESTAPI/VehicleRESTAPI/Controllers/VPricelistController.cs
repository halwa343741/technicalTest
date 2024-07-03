using Microsoft.AspNetCore.Mvc;
using VehicleRESTAPI.Attributes;
using VehicleRESTAPI.Repo.Vehicle.Interface;
using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VPricelistController : ControllerBase
    {
        private readonly IvPricelist repo;
        private readonly IvModel modelRepo;
        private readonly IvYear yearRepo;

        public VPricelistController(IvPricelist repo, IvYear yearRepo, IvModel modelRepo) {
            this.repo = repo;
            this.yearRepo = yearRepo;
            this.modelRepo = modelRepo;
        }

        [JwtAdmin]
        [HttpPut("create")]
        public async Task<IActionResult> Create(VPricelistModel dto) {
            try {
                var year = yearRepo.read().SingleOrDefault(y => y.id.Equals(dto.year_id));
                if (year == null) return BadRequest("Year id invalid!");

                var model = modelRepo.read().SingleOrDefault(y => y.id.Equals(dto.model_id));
                if (model== null) return BadRequest("Model id invalid!");

                if (repo.isCodeExists(dto)) return BadRequest("Pricelist code already exists!");

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
                if (result == null) return BadRequest("Brand not found!");
                return Ok(result);
            }
            catch {
                return BadRequest("Something went wrong");
            }
        }

        [Jwt]
        [HttpGet("reads")]
        public IActionResult Reads() {
            try {
                var result = repo.read().ToList();
                if (result == null) return BadRequest("Brand not found!");
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
        public async Task<IActionResult> Update([FromBody] VPricelistModel dto, [FromQuery] int id) {
            try {
                dto.id = id;
                var year = yearRepo.read().SingleOrDefault(y => y.id.Equals(dto.year_id));
                if (year == null) return BadRequest("Year id invalid!");

                var model = modelRepo.read().SingleOrDefault(y => y.id.Equals(dto.model_id));
                if (model == null) return BadRequest("Model id invalid!");

                if (repo.isCodeExists(dto)) return BadRequest("Pricelist code already exists!");

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
