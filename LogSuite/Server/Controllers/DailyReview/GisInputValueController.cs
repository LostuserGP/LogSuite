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
    public class GisInputValueController : Controller
    {
        private readonly IGisInputValueRepository _repository;

        public GisInputValueController(IGisInputValueRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GisInputValueDTO dto)
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
                        ErrorMessage = "GisInputValue with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new GisInputValue"
                });
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _repository.Delete(id);
            return result switch
            {
                0 => BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status404NotFound
                }),
                -1 => BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete GisInputValue with something assigned",
                    StatusCode = StatusCodes.Status409Conflict
                }),
                _ => Ok()
            };
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entity);
        }

        [HttpGet("bygisidpaged/{id:int}")]
        public async Task<IActionResult> Get([FromQuery] Params parameters, int id)
        {
            var pagedEntities = await _repository.GetPagedByGisId(id, parameters);
            if (pagedEntities == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            var entities = pagedEntities.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedEntities.MetaData));
            return Ok(entities);
        }

        [HttpGet("bygisidondate/{id:int}")]
        public async Task<IActionResult> Get([FromQuery] DateOnly date, int id)
        {
            var entity = await _repository.GetOnDateByGisId(id, date);
            if (entity == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entity);
        }

        [HttpGet("bygisidondaterange/{id:int}")]
        public async Task<IActionResult> Get([FromQuery] DateOnly dateStart, [FromQuery] DateOnly dateEnd, int id)
        {
            var entities = await _repository.GetOnDateRangeByGisId(id, dateStart, dateEnd);
            if (entities == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id or values not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entities);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update([FromBody] GisInputValueDTO dto, long id)
        {
            if (id != dto.Id)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            if (!ModelState.IsValid)
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while updating GisInputValue"
                });
            var isUnique = await _repository.IsUnique(dto, dto.Id);
            if (isUnique != null)
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "GisInputValue with such fields already exist",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            var result = await _repository.Update(dto);
            return Ok(result);

        }

    }
}
