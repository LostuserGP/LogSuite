using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.Operativka;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IGisInputNameService
    {
        Task<GisInputNameDTO> Get(int id);
        Task<PagingResponse<GisInputNameDTO>> GetAllByGisId(int id, Params parameters);
        Task<GisInputNameDTO> Create(GisInputNameDTO dto);
        Task<GisInputNameDTO> Update(GisInputNameDTO dto);
        Task<bool> Delete(int id);
    }
}
