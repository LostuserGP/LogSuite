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
using LogSuite.Shared.Models.CredRisk;

namespace LogSuite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingExternalController : Controller
    {
        private readonly IRatingExternalRepository _repository;

        public RatingExternalController(IRatingExternalRepository repository)
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
            var ratings = await _repository.GetAll(counterpartyId.Value);
            return Ok(ratings);
        }

        [HttpGet("{ratingId}")]
        public async Task<IActionResult> Get(int? ratingId)
        {
            if (ratingId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Rating Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var rating = await _repository.Get(ratingId.Value);
            if (rating == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Rating Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(rating);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RatingExternalDTO ratingDTO)
        {
            if (ModelState.IsValid)
            {
                var isUnique = await _repository.IsUnique(ratingDTO);
                if (isUnique == null)
                {
                    var result = await _repository.Create(ratingDTO);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErrorModel()
                    {
                        Title = "",
                        ErrorMessage = "Rating with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new Rating"
                });
            }
        }

        [HttpPut("{ratingId}")]
        public async Task<IActionResult> Update([FromBody] RatingExternalDTO ratingDTO, int? ratingId)
        {
            if (ratingId == null || ratingId != ratingDTO.Id)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Rating Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            if (ModelState.IsValid)
            {
                var isUnique = await _repository.IsUnique(ratingDTO, ratingDTO.Id);
                if (isUnique == null)
                {
                    var result = await _repository.Update(ratingDTO);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErrorModel()
                    {
                        Title = "",
                        ErrorMessage = "Rating with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new Rating"
                });
            }
        }

        [HttpDelete("{ratingId}")]
        public async Task<IActionResult> Delete(int? ratingId)
        {
            if (ratingId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Rating Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var result = await _repository.Delete(ratingId.Value);
            if (result == 0)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Rating Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            if (result == -1)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete Rating",
                    StatusCode = StatusCodes.Status409Conflict
                });
            }
            return Ok();
        }
    }
}
