using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Client.Services.DailyReview
{
    public interface IGisCountryAddonValueService
    {
        Task<PagingResponse<GisCountryAddonValueDto>> GetPagedByGisAddonId(int gisCountryAddonId, Params parameters);
        Task<GisCountryAddonValueDto> GetOnDateByGisCountryAddonId(int gisCountryAddonId, DateOnly date);
        Task<IEnumerable<GisCountryAddonValueDto>> GetOnDateRangeByGisAddonId(int gisCountryAddonId, DateOnly dateStart, DateOnly dateEnd);
        Task<GisCountryAddonValueDto> Get(int id);
        Task<GisCountryAddonValueDto> Create(GisCountryAddonValueDto dto);
        Task<GisCountryAddonValueDto> Update(GisCountryAddonValueDto dto);
        Task<bool> Delete(int id);
    }
}
