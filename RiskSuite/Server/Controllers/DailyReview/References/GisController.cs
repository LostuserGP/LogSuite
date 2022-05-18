using Business.Repositories.IRepository;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using LogSuite.Business.Repositories.DailyReview.References;
using LogSuite.Business.Repositories.IRepository;

namespace LogSuite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GisController : Controller
    {
        private IGisRepository _repository;

        public GisController(IGisRepository repository)
        {
            _repository = repository;
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repository.GetAll();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Params parameters)
        {
            var pagedGises = await _repository.GetPaged(parameters);
            var gises = pagedGises.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedGises.MetaData));
            return Ok(gises);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Gis Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var entity = await _repository.Get(id.Value);
            if (entity == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Gis Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GisDTO dto)
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
                        ErrorMessage = "Gis with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new gis"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] GisDTO dto, int? id)
        {
            if (id == null || id != dto.Id)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid gis Id",
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
                        ErrorMessage = "Gis with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while updating gis"
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
                    ErrorMessage = "Invalid gis Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var result = await _repository.Delete(id.Value);
            if (result == 0)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid gis Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            if (result == -1)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete gis with countries assigned",
                    StatusCode = StatusCodes.Status409Conflict
                });
            }
            return Ok();
        }

        [HttpGet("ondaterange")]
        public async Task<IActionResult> Get([FromQuery] DateTime startDate, [FromQuery] DateTime finishDate)
        {
            var entities = await _repository.GetOnDateRange(startDate, finishDate);
            if (entities == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid DateRange or values not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entities);
        }
    }
}
