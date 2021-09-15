using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface ICounterpartyService
    {
        public Task<IEnumerable<CounterpartyDTO>> Getall();
        public Task<CounterpartyDTO> Get(int counterpartyId);
        public Task<PagingResponse<CounterpartyDTO>> Getall(Params parameters);
    }
}
