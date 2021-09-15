using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository.References
{
    public interface ICountryRepository
    {
        Task<CountryDTO> Create(CountryDTO dto);
        Task<CountryDTO> Update(CountryDTO dto);
        Task<CountryDTO> Get(int id);
        Task<int> Delete(int id);
        Task<IEnumerable<CountryDTO>> GetAll();
        Task<CountryDTO> IsUnique(CountryDTO dto, int id = 0);
        Task<PagedList<CountryDTO>> GetPaged(Params parameters);
    }
}
