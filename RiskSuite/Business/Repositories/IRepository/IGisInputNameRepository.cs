using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IGisInputNameRepository
    {
        Task<GisInputNameDTO> Create(GisInputNameDTO dto);
        Task<GisInputNameDTO> Update(GisInputNameDTO dto);
        Task<GisInputNameDTO> Get(int id);
        Task<int> Delete(int id);
        Task<GisInputNameDTO> IsUnique(GisInputNameDTO dto, int id = 0);
        Task<PagedList<GisInputNameDTO>> GetPaged(Params parameters);
        Task<IEnumerable<GisInputNameDTO>> GetAllByGisId(int gisId);
        Task<PagedList<GisInputNameDTO>> GetPagedByGisId(int gisId, Params parameters);
    }
}
