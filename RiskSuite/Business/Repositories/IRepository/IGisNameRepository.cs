using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models.Operativka;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IGisNameRepository
    {
        Task<GisNameDTO> Create(GisNameDTO dto);
        Task<GisNameDTO> Update(GisNameDTO dto);
        Task<GisNameDTO> Get(int id);
        Task<int> Delete(int id);
        Task<PagedList<GisNameDTO>> GetByGisId(int gisId, Params parameters);
        Task<GisNameDTO> IsUnique(GisNameDTO dto, int id = 0);
    }
}
