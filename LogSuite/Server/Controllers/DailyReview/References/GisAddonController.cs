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
    public class GisAddonController : Controller
    {
        private readonly IGisAddonRepository _repository;

        public GisAddonController(IGisAddonRepository repository)
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
            var pagedEntities = await _repository.GetPaged(parameters);
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
        public async Task<IActionResult> GetPagedByGisId(int gisId, [FromQuery] Params parameters)
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
                    ErrorMessage = "Invalid GisAddon Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GisAddonDTO dto)
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
                        ErrorMessage = "GisAddon with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }

            return BadRequest(new ErrorModel()
            {
                ErrorMessage = "Error while creating new GisAddon"
            });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromBody] GisAddonDTO dto, int id)
        {
            if (id != dto.Id)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisAddon Id",
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
                    ErrorMessage = "GisAddon with such fields already exist",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            }

            return BadRequest(new ErrorModel()
            {
                ErrorMessage = "Error while updating GisAddon"
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.Delete(id);
            return result switch
            {
                0 => BadRequest(new ErrorModel()
                {
                    Title = "", ErrorMessage = "Invalid GisAddon Id", StatusCode = StatusCodes.Status404NotFound
                }),
                -1 => BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete GisAddon with countries assigned",
                    StatusCode = StatusCodes.Status409Conflict
                }),
                _ => Ok()
            };
        }
    }
}
