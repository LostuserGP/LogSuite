using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.Operativka;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IGisAddonService
    {
        Task<PagingResponse<GisAddonDTO>> GetPaged(Params parameters);
        Task<IEnumerable<GisAddonDTO>> GetAll();
        Task<IEnumerable<GisAddonDTO>> GetAllByGisId(int gisId);
        Task<PagingResponse<GisAddonDTO>> GetPagedByGisId(int gisId, Params parameters);
        Task<GisAddonDTO> Get(int id);
        Task<GisAddonDTO> Create(GisAddonDTO dto);
        Task<GisAddonDTO> Update(GisAddonDTO dto);
        Task<bool> Delete(int id);
    }
}
