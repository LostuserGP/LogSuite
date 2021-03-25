using Business.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _departmentRepository.GetAll();
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
    }
}
