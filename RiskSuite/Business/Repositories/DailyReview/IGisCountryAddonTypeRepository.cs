using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview.References
{
    public interface IGisCountryAddonTypeRepository : IRepositoryBase<GisCountryAddonTypeDto>
    {
        Task<PagedList<GisCountryAddonTypeDto>> GetPagedByGisCountryAddonId(int gisCountryAddonId, Params parameters);
    }
}
