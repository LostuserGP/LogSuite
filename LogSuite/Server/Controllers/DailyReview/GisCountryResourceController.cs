using System;
using System.Linq;
using System.Threading.Tasks;
using LogSuite.Business.Repositories.DailyReview;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LogSuite.Server.Controllers.DailyReview
{
    [ApiController]
    [Route("api/[controller]")]
    public class GisCountryResourceController : Controller
    {
        private readonly IGisCountryResourceRepository _repository;

        public GisCountryResourceController(IGisCountryResourceRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GisCountryResourceDTO dto)
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
                        ErrorMessage = "GisCountryResource with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new GisCountryResource"
                });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.Delete(id);
            return result switch
            {
                0 => BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountryResource Id",
                    StatusCode = StatusCodes.Status404NotFound
                }),
                -1 => BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete GisCountryResource with something assigned",
                    StatusCode = StatusCodes.Status409Conflict
                }),
                _ => Ok()
            };
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
                    ErrorMessage = "Invalid GisCountryResource Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entity);
        }

        [HttpGet("bygiscountryidpaged/{id:int}")]
        public async Task<IActionResult> Get([FromQuery] Params parameters, int id)
        {
            var pagedEntities = await _repository.GetPagedByGisCountryId(id, parameters);
            if (pagedEntities == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountry Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            var entities = pagedEntities.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedEntities.MetaData));
            return Ok(entities);
        }

        [HttpGet("bygiscountryidondate/{id:int}")]
        public async Task<IActionResult> Get([FromQuery] DateOnly date, int id)
        {
            var entity = await _repository.GetOnDateByGisCountryId(id, date);
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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromBody] GisCountryResourceDTO dto, int id)
        {
            if (id != dto.Id)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountryResource Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            if (!ModelState.IsValid)
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while updating GisCountryResource"
                });
            var isUnique = await _repository.IsUnique(dto, dto.Id);
            if (isUnique != null)
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "GisCountryResource with such fields already exist",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            var result = await _repository.Update(dto);
            return Ok(result);
        }

    }
}
