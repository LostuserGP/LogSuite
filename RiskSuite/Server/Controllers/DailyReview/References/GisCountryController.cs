using Business.Repositories.IRepository;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using LogSuite.Business.Repositories.DailyReview.References;
using LogSuite.Business.Repositories.IRepository;

namespace LogSuite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GisCountryController : Controller
    {
        private IGisCountryRepository _repository;

        public GisCountryController(IGisCountryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repository.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] Params parameters)
        {
            var pagedEntities = await _repository.GetAllPaged(parameters);
            var entities = pagedEntities.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedEntities.MetaData));
            return Ok(entities);
        }

        [HttpGet]
        [Route("bycountryid/{countryId}")]
        public async Task<IActionResult> GetAllByCountryId(int? countryId)
        {
            if (countryId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid country Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var result = await _repository.GetAllByCountryId(countryId.Value);
            return Ok(result);
        }

        [HttpGet]
        [Route("bycountryid/{countryId}/paged")]
        public async Task<IActionResult> GetAllPagedByCountryId(int? countryId, [FromQuery] Params parameters)
        {
            if (countryId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid country Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var pagedEntities = await _repository.GetPagedByCountryId(countryId.Value, parameters);
            var entities = pagedEntities.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedEntities.MetaData));
            return Ok(entities);
        }

        [HttpGet]
        [Route("bygisid/{gisId}")]
        public async Task<IActionResult> GetAllByGisId(int? gisId)
        {
            if (gisId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid gis Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var result = await _repository.GetAllByGisId(gisId.Value);
            return Ok(result);
        }

        [HttpGet]
        [Route("bygisid/{gisId}/paged")]
        public async Task<IActionResult> GetAllPagedByGisId(int? gisId, [FromQuery] Params parameters)
        {
            if (gisId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid country Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var pagedEntities = await _repository.GetPagedByGisId(gisId.Value, parameters);
            var entities = pagedEntities.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedEntities.MetaData));
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountry Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var entity = await _repository.Get(id.Value);
            if (entity == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountry Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GisCountryDTO dto)
        {
            if (ModelState.IsValid)
            {
                var isUnique = await _repository.IsUnique(dto);
                if (isUnique == null)
                {
                    var result = await _repository.Create(dto);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErrorModel()
                    {
                        Title = "",
                        ErrorMessage = "GisCountry with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new gisCountry"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] GisCountryDTO dto, int? id)
        {
            if (id == null || id != dto.Id)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid gisCountry Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            if (ModelState.IsValid)
            {
                var isUnique = await _repository.IsUnique(dto, dto.Id);
                if (isUnique == null)
                {
                    var result = await _repository.Update(dto);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErrorModel()
                    {
                        Title = "",
                        ErrorMessage = "GisCountry with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while updating gisCountry"
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid gisCountry Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var result = await _repository.Delete(id.Value);
            if (result == 0)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid gisCountry Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            if (result == -1)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete gisCountry with countries assigned",
                    StatusCode = StatusCodes.Status409Conflict
                });
            }
            return Ok();
        }
    }
}
