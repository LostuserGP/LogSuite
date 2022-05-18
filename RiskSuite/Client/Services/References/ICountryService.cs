using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> Getall();
        Task<PagingResponse<CountryDTO>> Getall(Params parameters);
        Task<CountryDTO> Get(int id);
        Task<CountryDTO> Create(CountryDTO dto);
        Task<CountryDTO> Update(CountryDTO dto);
        Task<bool> Delete(int id);
    }
}
