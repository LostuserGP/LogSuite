using Business.Repositories.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LogSuite.Server.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogSuite.Business.Repositories.IRepository;

namespace LogSuite.Server.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class CounterpartyController : Controller
    {
        private readonly ICounterpartyRepository _repository;

        public CounterpartyController(ICounterpartyRepository counterpartyRepository)
        {
            _repository = counterpartyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCounterparties([FromQuery] Params parameters)
        {
            var pagedCounterparties = await _repository.GetPaged(parameters);
            var counterparties = pagedCounterparties.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedCounterparties.MetaData));
            return Ok(counterparties);
        }

        [HttpGet("{counterpartyId}")]
        public async Task<IActionResult> Get(int? counterpartyId)
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
            var counterparty = await _repository.Get(counterpartyId.Value);
            if (counterparty == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Counterparty Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(counterparty);
        }

    }
}
