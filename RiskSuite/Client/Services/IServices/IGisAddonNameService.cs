using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.Operativka;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IGisAddonNameService
    {
        Task<PagingResponse<GisAddonNameDTO>> GetPaged(Params parameters);
        Task<IEnumerable<GisAddonNameDTO>> GetAll();
        Task<IEnumerable<GisAddonNameDTO>> GetAllByGisAddonId(int gisAddonId);
        Task<PagingResponse<GisAddonNameDTO>> GetPagedByGisAddonId(int gisAddonId, Params parameters);
        Task<GisAddonNameDTO> Get(int id);
        Task<GisAddonNameDTO> Create(GisAddonNameDTO dto);
        Task<GisAddonNameDTO> Update(GisAddonNameDTO dto);
        Task<bool> Delete(int id);
    }
}
