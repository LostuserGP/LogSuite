using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview
{
    public interface IGisCountryAddonTypeRepository : IRepositoryBase<GisCountryAddonTypeDto>
    {
        Task<PagedList<GisCountryAddonTypeDto>> GetPagedByGisCountryAddonId(int gisCountryAddonId, Params parameters);
    }
}
