using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview
{
    public interface IGisCountryAddonValueRepository : IRepositoryValueBase<GisCountryAddonValueDto>
    {
        Task<PagedList<GisCountryAddonValueDto>> GetPagedByGisCountryAddonId(int gisCountryAddonId, Params parameters);
        Task<GisCountryAddonValueDto> GetOnDateByGisCountryAddonId(int gisCountryAddonId, DateOnly date);
        Task<IEnumerable<GisCountryAddonValueDto>> GetOnDateRangeByGisCountryAddonId(int gisCountryAddonId, DateOnly startDate, DateOnly finishDate);
    }
}
