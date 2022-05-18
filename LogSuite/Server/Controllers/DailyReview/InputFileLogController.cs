using System.Threading.Tasks;
using LogSuite.Business.Repositories.DailyReview;
using LogSuite.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogSuite.Server.Controllers.DailyReview
{
    [ApiController]
    [Route("api/[controller]")]
    public class InputFileLogController : Controller
    {
        private readonly IInputFileLogRepository _repository;

        public InputFileLogController(IInputFileLogRepository repository)
        {
            _repository = repository;
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
                    ErrorMessage = "Invalid Gis Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entity);
        }

    }
}
