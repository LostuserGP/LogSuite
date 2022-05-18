using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview
{
    public interface IGisAddonValueRepository : IRepositoryValueBase<GisAddonValueDTO>
    {
        Task<PagedList<GisAddonValueDTO>> GetPagedByGisAddonId(int gisAddonId, Params parameters);
        Task<GisAddonValueDTO> GetOnDateByGisAddonId(int gisAddonId, DateOnly date);
        Task<List<GisAddonValueDTO>> GetOnDateRangeByGisAddonId(int gisAddonId, DateOnly startDate, DateOnly finishDate);
    }
}
