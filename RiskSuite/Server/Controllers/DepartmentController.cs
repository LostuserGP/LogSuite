using Business.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RiskSuite.Shared;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskSuite.Server.Controllers
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

        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetDepartment(int? departmentId)
        {
            if (departmentId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Department Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var department = await _departmentRepository.Get(departmentId.Value);
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
        public async Task<IActionResult> Create([FromBody] DepartmentDTO departmentDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _departmentRepository.Create(departmentDTO);
                return Ok(result);
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new department"
                });
            }
        }

        [HttpPut("{departmentId}")]
        public async Task<IActionResult> Update([FromBody] DepartmentDTO departmentDTO, int? departmentId)
        {
            if (departmentId == null || departmentId != departmentDTO.Id)
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
                var isUnique = await _departmentRepository.IsUnique(departmentDTO, departmentDTO.Id);
                if (isUnique == null)
                {
                    var result = await _departmentRepository.Update(departmentDTO);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErrorModel()
                    {
                        Title = "",
                        ErrorMessage = "Department with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new department"
                });
            }
        }
    }
}
