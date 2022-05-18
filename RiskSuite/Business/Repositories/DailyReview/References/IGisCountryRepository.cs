using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.IRepository
{
    public interface IGisCountryRepository : IRepositoryBase<GisCountryDTO>
    {
        Task<PagedList<GisCountryDTO>> GetPagedByCountryId(int countryId, Params parameters);
        Task<PagedList<GisCountryDTO>> GetPagedByGisId(int gisId, Params parameters);
        Task<IEnumerable<GisCountryDTO>> GetAllByGisId(int id);
        Task<IEnumerable<GisCountryDTO>> GetAllByCountryId(int id);
    }
}
