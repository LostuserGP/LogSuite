using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Client.Services.DailyReview.References
{
    public interface IGisCountryAddonService
    {
        Task<PagingResponse<GisCountryAddonDto>> GetPaged(Params parameters);
        Task<IEnumerable<GisCountryAddonDto>> GetAll();
        Task<IEnumerable<GisCountryAddonDto>> GetAllByGisCountryId(int gisCountryId);
        Task<IEnumerable<GisCountryAddonDto>> GetAllByGisId(int gisId);
        Task<PagingResponse<GisCountryAddonDto>> GetAllPagedByGisCountryId(int gisCountryId, Params parameters);
        Task<GisCountryAddonDto> Get(int id);
        Task<GisCountryAddonDto> Create(GisCountryAddonDto dto);
        Task<GisCountryAddonDto> Update(GisCountryAddonDto dto);
        Task<bool> Delete(int id);
    }
}
