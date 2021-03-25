using RiskSuite.Client.Helpers;
using RiskSuite.Shared;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskSuite.Client.Services.IServices
{
    public interface ICounterpartyService
    {
        public Task<IEnumerable<CounterpartyDTO>> Getall();
        public Task<CounterpartyDTO> Get(int counterpartyId);
        public Task<PagingResponse<CounterpartyDTO>> Getall(Params parameters);
    }
}
