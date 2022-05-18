using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.References;

namespace LogSuite.Client.Services.References
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> GetAll();
        Task<PagingResponse<CountryDTO>> GetAll(Params parameters);
        Task<CountryDTO> Get(int id);
        Task<CountryDTO> Create(CountryDTO dto);
        Task<CountryDTO> Update(CountryDTO dto);
        Task<bool> Delete(int id);
    }
}
