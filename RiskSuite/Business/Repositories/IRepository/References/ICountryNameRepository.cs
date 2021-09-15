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
    public interface ICountryNameRepository
    {
        Task<CountryNameDTO> Create(CountryNameDTO dto);
        Task<CountryNameDTO> Update(CountryNameDTO dto);
        Task<CountryNameDTO> Get(int id);
        Task<int> Delete(int id);
        Task<IEnumerable<CountryNameDTO>> GetAll();
        Task<IEnumerable<CountryNameDTO>> GetByCountryId(int countryId);
        Task<PagedList<CountryNameDTO>> GetPagedByCountryId(int countryId, Params parameters);
        Task<CountryNameDTO> IsUnique(CountryNameDTO dto, int id = 0);
    }
}
