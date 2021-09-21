using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IGisOutputNameRepository
    {
        Task<GisOutputNameDTO> Create(GisOutputNameDTO dto);
        Task<GisOutputNameDTO> Update(GisOutputNameDTO dto);
        Task<GisOutputNameDTO> Get(int id);
        Task<int> Delete(int id);
        Task<IEnumerable<GisOutputNameDTO>> GetAll();
        Task<GisOutputNameDTO> IsUnique(GisOutputNameDTO dto, int id = 0);
        Task<PagedList<GisOutputNameDTO>> GetPaged(Params parameters);
        Task<IEnumerable<GisOutputNameDTO>> GetAllByGisId(int gisId);
        Task<PagedList<GisOutputNameDTO>> GetPagedByGisId(int gisId, Params parameters);
    }
}
