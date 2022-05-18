using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview
{
    public interface IGisOutputValueRepository : IRepositoryValueBase<GisOutputValueDTO>
    {
        Task<PagedList<GisOutputValueDTO>> GetPagedByGisId(int gisId, Params parameters);
        Task<GisOutputValueDTO> GetOnDateByGisId(int gisId, DateOnly date);
        Task<List<GisOutputValueDTO>> GetOnDateRangeByGisId(int gisId, DateOnly startDate, DateOnly finishDate);
    }
}
