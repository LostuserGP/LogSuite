using System.Linq;
using System.Threading.Tasks;
using LogSuite.Business.Repositories.DailyReview.References;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LogSuite.Server.Controllers.DailyReview.References
{
    [ApiController]
    [Route("api/[controller]")]
    public class GisCountryController : Controller
    {
        private readonly IGisCountryRepository _repository;

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
        [Route("bycountryid/{countryId:int}")]
        public async Task<IActionResult> GetAllByCountryId(int countryId)
        {
            var result = await _repository.GetAllByCountryId(countryId);
            return Ok(result);
        }

        [HttpGet]
        [Route("bycountryid/{countryId:int}/paged")]
        public async Task<IActionResult> GetAllPagedByCountryId(int countryId, [FromQuery] Params parameters)
        {
            var pagedEntities = await _repository.GetPagedByCountryId(countryId, parameters);
            var entities = pagedEntities.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedEntities.MetaData));
            return Ok(entities);
        }

        [HttpGet]
        [Route("bygisid/{gisId:int}")]
        public async Task<IActionResult> GetAllByGisId(int gisId)
        {
            var result = await _repository.GetAllByGisId(gisId);
            return Ok(result);
        }

        [HttpGet]
        [Route("bygisid/{gisId:int}/paged")]
        public async Task<IActionResult> GetAllPagedByGisId(int gisId, [FromQuery] Params parameters)
        {
            var pagedEntities = await _repository.GetPagedByGisId(gisId, parameters);
            var entities = pagedEntities.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedEntities.MetaData));
            return Ok(entities);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _repository.Get(id);
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
                if (isUnique != null)
                    return BadRequest(new ErrorModel()
                    {
                        Title = "",
                        ErrorMessage = "GisCountry with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                var result = await _repository.Create(dto);
                return Ok(result);

            }

            return BadRequest(new ErrorModel()
            {
                ErrorMessage = "Error while creating new gisCountry"
            });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromBody] GisCountryDTO dto, int id)
        {
            if (id != dto.Id)
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

                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "GisCountry with such fields already exist",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            }

            return BadRequest(new ErrorModel()
            {
                ErrorMessage = "Error while updating gisCountry"
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
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
            var result = await _repository.Delete(id);
            return result switch
            {
                0 => BadRequest(new ErrorModel()
                {
                    Title = "", ErrorMessage = "Invalid gisCountry Id", StatusCode = StatusCodes.Status404NotFound
                }),
                -1 => BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete gisCountry with countries assigned",
                    StatusCode = StatusCodes.Status409Conflict
                }),
                _ => Ok()
            };
        }
    }
}
