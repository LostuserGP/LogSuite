using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview.References
{
    public interface IGisAddonRepository : IRepositoryBase<GisAddonDTO>
    {
        Task<IEnumerable<GisAddonDTO>> GetAllByGisId(int gisId);
        Task<PagedList<GisAddonDTO>> GetPagedByGisId(int gisId, Params parameters);
    }
}
