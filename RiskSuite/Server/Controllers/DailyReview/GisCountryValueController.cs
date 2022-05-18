using Business.Repositories.IRepository;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using LogSuite.Business.Repositories.DailyReview;
using LogSuite.Business.Repositories.IRepository;

namespace LogSuite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GisCountryValueController : Controller
    {
        private IGisCountryValueRepository _repository;

        public GisCountryValueController(IGisCountryValueRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GisCountryValueDTO dto)
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
                        ErrorMessage = "GisCountryValue with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new GisCountryValue"
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
                    ErrorMessage = "Invalid GisCountryValue Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var result = await _repository.Delete(id.Value);
            if (result == 0)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountryValue Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            if (result == -1)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete GisCountryValue with something assigned",
                    StatusCode = StatusCodes.Status409Conflict
                });
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountryValue Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var entity = await _repository.Get(id.Value);
            if (entity == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountryValue Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entity);
        }

        [HttpGet("bygiscountryidpaged/{id}")]
        public async Task<IActionResult> Get([FromQuery] Params parameters, int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountryValue Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var pagedEntities = await _repository.GetPagedByGisCountryId(id.Value, parameters);
            if (pagedEntities == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountryValue Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            var entities = pagedEntities.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedEntities.MetaData));
            return Ok(entities);
        }

        [HttpGet("bygiscountryidondate/{id}")]
        public async Task<IActionResult> Get([FromQuery] DateTime date, int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountryValue Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var entity = await _repository.GetOnDateByGisCountryId(id.Value, date);
            if (entity == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountryValue Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entity);
        }

        [HttpGet("bygiscountryidondaterange/{id}")]
        public async Task<IActionResult> Get([FromQuery] DateTime dateStart, [FromQuery] DateTime dateEnd, int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountryValue Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var entities = await _repository.GetOnDateRangeByGisCountryId(id.Value, dateStart, dateEnd);
            if (entities == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountryValue Id or values not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entities);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] GisCountryValueDTO dto, int? id)
        {
            if (id == null || id != dto.Id)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisCountryValue Id",
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
                        ErrorMessage = "GisCountryValue with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while updating GisCountryValue"
                });
            }
        }

    }
}
