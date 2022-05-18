using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview.References
{
    public interface IGisCountryAddonRepository : IRepositoryBase<GisCountryAddonDto>
    {
        Task<PagedList<GisCountryAddonDto>> GetPagedByGisCountryId(int countryId, Params parameters);
    }
}
