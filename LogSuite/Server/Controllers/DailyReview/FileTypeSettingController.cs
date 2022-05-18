using System.Linq;
using System.Threading.Tasks;
using LogSuite.Business.Repositories.DailyReview.References;
using LogSuite.Shared;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LogSuite.Server.Controllers.DailyReview
{
    [ApiController]
    [Route("api/references/[controller]")]
    public class FileTypeSettingController : Controller
    {
        private readonly IFileTypeSettingRepository _repository;

        public FileTypeSettingController(IFileTypeSettingRepository repository)
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
            var pagedFileTypes = await _repository.GetPaged(parameters);
            var fileTypes = pagedFileTypes.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedFileTypes.MetaData));
            return Ok(fileTypes);
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
                    ErrorMessage = "Invalid FileType Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FileTypeSettingDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Create(dto);
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new FileType"
                });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromBody] FileTypeSettingDTO dto, int id)
        {
            if (id != dto.Id)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid FileType Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            if (!ModelState.IsValid)
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while updating FileType"
                });
            var result = await _repository.Update(dto);
            return Ok(result);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.Delete(id);
            return result switch
            {
                0 => BadRequest(new ErrorModel()
                {
                    Title = "", ErrorMessage = "Invalid FileType Id", StatusCode = StatusCodes.Status404NotFound
                }),
                -1 => BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete FileType with something assigned",
                    StatusCode = StatusCodes.Status409Conflict
                }),
                _ => Ok()
            };
        }
    }
}
