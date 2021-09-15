using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.Operativka;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IGisNameService
    {
        Task<GisNameDTO> Get(int id);
        Task<PagingResponse<GisNameDTO>> GetAllByGisId(int id, Params parameters);
        Task<GisNameDTO> Create(GisNameDTO dto);
        Task<GisNameDTO> Update(GisNameDTO dto);
        Task<bool> Delete(int id);
    }
}
