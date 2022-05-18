using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Client.Services.DailyReview
{
    public interface IGisCountryAddonTypeService
    {
        Task<PagingResponse<GisCountryAddonTypeDto>> GetPagedByGisCountryAddonId(int gisCountryAddonId, Params parameters);
        Task<GisCountryAddonTypeDto> GetOnDateByGisCountryAddonId(int gisCountryAddonId, DateOnly date);
        Task<List<GisCountryAddonTypeDto>> GetOnDateRangeByGisCountryAddonId(int gisCountryAddonId, DateOnly dateStart, DateOnly dateEnd);
        Task<GisCountryAddonTypeDto> Get(int id);
        Task<GisCountryAddonTypeDto> Create(GisCountryAddonTypeDto dto);
        Task<GisCountryAddonTypeDto> Update(GisCountryAddonTypeDto dto);
        Task<bool> Delete(int id);
    }
}
