using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.IRepository
{
    public interface IGisOutputValueRepository : IRepositoryBase<GisOutputValueDTO>
    {
        Task<PagedList<GisOutputValueDTO>> GetPagedByGisId(int gisId, Params parameters);
        Task<GisOutputValueDTO> GetOnDateByGisId(int gisId, DateTime date);
        Task<List<GisOutputValueDTO>> GetOnDateRangeByGisId(int gisId, DateTime startDate, DateTime finishDate);
    }
}
