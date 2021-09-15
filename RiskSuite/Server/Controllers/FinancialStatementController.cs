using Business.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialStatementController : Controller
    {
        private readonly IFinancialStatementRepository _repository;

        public FinancialStatementController(IFinancialStatementRepository repository)
        {
            _repository = repository;
        }

        [Route("all/{counterpartyId}")]
        [HttpGet]
        public async Task<IActionResult> GetAll(int? counterpartyId)
        {
            if (counterpartyId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Counterparty Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var fss = await _repository.GetAll(counterpartyId.Value);
            return Ok(fss);
        }

        [HttpGet("{fsId}")]
        public async Task<IActionResult> Get(int? fsId)
        {
            if (fsId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid FS Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var fs = await _repository.Get(fsId.Value);
            if (fs == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid FS Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(fs);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FinancialStatementDTO fsDTO)
        {
            if (ModelState.IsValid)
            {
                var isUnique = await _repository.IsUnique(fsDTO);
                if (isUnique == null)
                {
                    var result = await _repository.Create(fsDTO);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErrorModel()
                    {
                        Title = "",
                        ErrorMessage = "FS with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new FS"
                });
            }
        }

        [HttpPut("{fsId}")]
        public async Task<IActionResult> Update([FromBody] FinancialStatementDTO fsDTO, int? fsId)
        {
            if (fsId == null || fsId != fsDTO.Id)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid FS Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            if (ModelState.IsValid)
            {
                var isUnique = await _repository.IsUnique(fsDTO, fsDTO.Id);
                if (isUnique == null)
                {
                    var result = await _repository.Update(fsDTO);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErrorModel()
                    {
                        Title = "",
                        ErrorMessage = "FS with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new FS"
                });
            }
        }

        [HttpDelete("{fsId}")]
        public async Task<IActionResult> Delete(int? fsId)
        {
            if (fsId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid FS Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var result = await _repository.Delete(fsId.Value);
            if (result == 0)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid FS Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            if (result == -1)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete FS",
                    StatusCode = StatusCodes.Status409Conflict
                });
            }
            return Ok();
        }
    }
}
