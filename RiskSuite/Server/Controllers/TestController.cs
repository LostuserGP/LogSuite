
using Business.Repositories.IRepository;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LogSuite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private IGisRepository _repository;

        public TestController(IGisRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewValueListDTO dto)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "It works!",
                    ErrorMessage = "Gis with such fields already exist",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new gis"
                });
            }
        }
    }
}
