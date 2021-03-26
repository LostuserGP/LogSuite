using Business.Repositories.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RiskSuite.Server.Helpers;
using RiskSuite.Shared;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskSuite.Server.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class CounterpartyController : Controller
    {
        private readonly ICounterpartyRepository _counterpartyRepository;

        public CounterpartyController(ICounterpartyRepository counterpartyRepository)
        {
            _counterpartyRepository = counterpartyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCounterparties([FromQuery] Params parameters)
        {
            var pagedCounterparties = await _counterpartyRepository.GetPaged(parameters);
            var counterparties = pagedCounterparties.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedCounterparties.MetaData));
            return Ok(counterparties);
        }
    }
}
