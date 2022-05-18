using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview
{
    public interface IGisInputValueRepository : IRepositoryValueBase<GisInputValueDTO>
    {
        Task<PagedList<GisInputValueDTO>> GetPagedByGisId(int gisId, Params parameters);
        Task<GisInputValueDTO> GetOnDateByGisId(int gisId, DateOnly date);
        Task<List<GisInputValueDTO>> GetOnDateRangeByGisId(int gisId, DateOnly startDate, DateOnly finishDate);
    }
}
