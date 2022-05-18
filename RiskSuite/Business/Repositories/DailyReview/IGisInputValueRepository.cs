using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.IRepository
{
    public interface IGisInputValueRepository : IRepositoryBase<GisInputValueDTO>
    {
        Task<PagedList<GisInputValueDTO>> GetPagedByGisId(int gisId, Params parameters);
        Task<GisInputValueDTO> GetOnDateByGisId(int gisId, DateTime date);
        Task<List<GisInputValueDTO>> GetOnDateRangeByGisId(int gisId, DateTime startDate, DateTime finishDate);
    }
}
