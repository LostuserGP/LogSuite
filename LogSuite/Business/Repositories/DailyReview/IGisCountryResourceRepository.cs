using System;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview
{
    public interface IGisCountryResourceRepository : IRepositoryBase<GisCountryResourceDTO>
    {
        Task<PagedList<GisCountryResourceDTO>> GetPagedByGisCountryId(int gisCountryId, Params parameters);
        Task<GisCountryResourceDTO> GetOnDateByGisCountryId(int gisCountryId, DateOnly date);
    }
}
