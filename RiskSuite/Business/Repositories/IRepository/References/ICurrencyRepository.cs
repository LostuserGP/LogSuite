using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository.References
{
    public interface ICurrencyRepository
    {
        public Task<CurrencyDTO> Create(CurrencyDTO dto);
        public Task<CurrencyDTO> Update(CurrencyDTO dto);
        public Task<CurrencyDTO> Get(int id);
        public Task<int> Delete(int id);
        public Task<IEnumerable<CurrencyDTO>> GetAll();
        public Task<CurrencyDTO> IsUnique(CurrencyDTO dto, int id = 0);
    }
}
