using Business.Repositories.IRepository;
using LogSuite.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LogSuite.Business.Repositories.DailyReview;
using LogSuite.Business.Repositories.IRepository;

namespace LogSuite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InputFileLogController : Controller
    {
        private IInputFileLogRepository _repository;

        public InputFileLogController(IInputFileLogRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid InputFileLog Id",
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

    }
}
