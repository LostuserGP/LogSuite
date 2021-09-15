using LogSuite.Business;
using LogSuite.DataAccess.CredRisk;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface ICounterpartyRepository
    {
        public Task<CounterpartyDTO> Create(CounterpartyDTO counterpartyDTO);
        public Task<CounterpartyDTO> Update(int counterpartyId, CounterpartyDTO counterpartyDTO);
        public Task<CounterpartyDTO> Get(int counterpartyId);
        public Task<int> Delete(int counterpartyId);
        public Task<IEnumerable<CounterpartyDTO>> GetAll();
        public Task<CounterpartyDTO> IsUnique(string name, int counterpartyId = 0);
        public Task<PagedList<CounterpartyDTO>> GetPaged(Params parameters);
    }
}
