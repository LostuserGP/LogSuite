using Business.Repositories.IRepository;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using LogSuite.Business.Repositories.IRepository;
using LogSuite.Business.Repositories.References;

namespace LogSuite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _departmentRepository.GetAll();
            return Ok(departments);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments([FromQuery] Params parameters)
        {
            var pagedDepartments = await _departmentRepository.GetPaged(parameters);
            var departments = pagedDepartments.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedDepartments.MetaData));
            return Ok(departments);
        }

        [HttpGet("{departmentId:int}")]
        public async Task<IActionResult> GetDepartment(int departmentId)
        {
            var department = await _departmentRepository.Get(departmentId);
            if (department == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Department Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentDTO departmentDto)
        {
            if (ModelState.IsValid)
            {
                var isUnique = await _departmentRepository.IsUnique(departmentDto);
                if (isUnique == null)
                {
                    var result = await _departmentRepository.Create(departmentDto);
                    return Ok(result);
                }

                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Department with such fields already exist",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new department"
                });
            }
        }

        [HttpPut("{departmentId:int}")]
        public async Task<IActionResult> Update([FromBody] DepartmentDTO departmentDto, int departmentId)
        {
            if (departmentId != departmentDto.Id)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Department Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            if (ModelState.IsValid)
            {
                var isUnique = await _departmentRepository.IsUnique(departmentDto, departmentDto.Id);
                if (isUnique == null)
                {
                    var result = await _departmentRepository.Update(departmentDto);
                    return Ok(result);
                }

                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Department with such fields already exist",
                    StatusCode = StatusCodes.Status406NotAcceptable
                });
            }

            return BadRequest(new ErrorModel()
            {
                ErrorMessage = "Error while creating new department"
            });
        }

        [HttpDelete("{departmentId:int}")]
        public async Task<IActionResult> DeleteDepartment(int departmentId)
        {
            var result = await _departmentRepository.Delete(departmentId);
            return result switch
            {
                0 => BadRequest(new ErrorModel()
                {
                    Title = "", ErrorMessage = "Invalid Department Id", StatusCode = StatusCodes.Status404NotFound
                }),
                -1 => BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete department with accounts",
                    StatusCode = StatusCodes.Status409Conflict
                }),
                _ => Ok()
            };
        }
    }
}
