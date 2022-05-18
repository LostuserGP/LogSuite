using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using System;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IGisCountryResourceService
    {
        Task<PagingResponse<GisCountryResourceDTO>> GetPagedByGisCountryId(int gisCountryId, Params parameters);
        Task<GisCountryResourceDTO> GetOnDateByGisCountryId(int gisCountryId, DateTime date);
        Task<GisCountryResourceDTO> Get(int id);
        Task<GisCountryResourceDTO> Create(GisCountryResourceDTO dto);
        Task<GisCountryResourceDTO> Update(GisCountryResourceDTO dto);
        Task<bool> Delete(int id);
    }
}
