using Business.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LogSuite.DataAccess.CredRisk;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogSuite.Business.Repositories.IRepository.References;
using LogSuite.Shared.Models.CredRisk;

namespace LogSuite.Server.Controllers.References
{
    [ApiController]
    [Route("api/references/[controller]")]
    public class RatingAgencyController : Controller
    {
        private IRatingAgencyRepository _repository;

        public RatingAgencyController(IRatingAgencyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid RatingAgency Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var entity = await _repository.Get(id.Value);
            if (entity == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid RatingAgency Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RatingAgencyDTO dto)
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
                        ErrorMessage = "RatingAgency with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new RatingAgency"
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] RatingAgencyDTO dto, int? id)
        {
            if (id == null || id != dto.Id)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid RatingAgency Id",
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
                        ErrorMessage = "RatingAgency with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while updating RatingAgency"
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
                    ErrorMessage = "Invalid RatingAgency Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var result = await _repository.Delete(id.Value);
            if (result == 0)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid RatingAgency Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            if (result == -1)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete RatingAgency with ratings assigned",
                    StatusCode = StatusCodes.Status409Conflict
                });
            }
            return Ok();
        }
    }
}
