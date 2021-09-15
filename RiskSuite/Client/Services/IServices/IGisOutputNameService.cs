using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.Operativka;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IGisOutputNameService
    {
        Task<GisOutputNameDTO> Get(int id);
        Task<PagingResponse<GisOutputNameDTO>> GetAllByGisId(int id, Params parameters);
        Task<GisOutputNameDTO> Create(GisOutputNameDTO dto);
        Task<GisOutputNameDTO> Update(GisOutputNameDTO dto);
        Task<bool> Delete(int id);
    }
}
