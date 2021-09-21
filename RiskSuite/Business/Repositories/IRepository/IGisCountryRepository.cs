using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.DailyReview;
using LogSuite.Shared.Models.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IGisCountryRepository
    {
        Task<GisCountryDTO> Create(GisCountryDTO dto);
        Task<GisCountryDTO> Update(GisCountryDTO dto);
        Task<GisCountryDTO> Get(int id);
        Task<int> Delete(int id);
        Task<GisCountryDTO> IsUnique(GisCountryDTO dto, int id = 0);
        Task<PagedList<GisCountryDTO>> GetAllPagedByCountryId(int countryId, Params parameters);
        Task<PagedList<GisCountryDTO>> GetAllPagedByGisId(int gisId, Params parameters);
        Task<PagedList<GisCountryDTO>> GetAllPaged(Params parameters);
        Task<IEnumerable<GisCountryDTO>> GetAll();
        Task<IEnumerable<GisCountryDTO>> GetAllByGisId(int id);
        Task<IEnumerable<GisCountryDTO>> GetAllByCountryId(int id);
    }
}
