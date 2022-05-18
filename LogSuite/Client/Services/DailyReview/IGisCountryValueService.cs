using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Client.Services.DailyReview
{
    public interface IGisCountryValueService
    {
        Task<PagingResponse<GisCountryValueDTO>> GetPagedByGisCountryId(int gisCountryId, Params parameters);
        Task<GisCountryValueDTO> GetOnDateByGisCountryId(int gisCountryId, DateOnly date);
        Task<List<GisCountryValueDTO>> GetOnDateRangeByGisCountryId(int gisCountryId, DateOnly dateStart, DateOnly dateEnd);
        Task<GisCountryValueDTO> Get(int id);
        Task<GisCountryValueDTO> Create(GisCountryValueDTO dto);
        Task<GisCountryValueDTO> Update(GisCountryValueDTO dto);
        Task<bool> Delete(int id);
    }
}
