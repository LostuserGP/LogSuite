using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.IRepository
{
    public interface IGisCountryValueRepository : IRepositoryBase<GisCountryValueDTO>
    {
        Task<PagedList<GisCountryValueDTO>> GetPagedByGisCountryId(int gisCountryId, Params parameters);
        Task<GisCountryValueDTO> GetOnDateByGisCountryId(int gisCountryId, DateTime date);
        Task<IEnumerable<GisCountryValueDTO>> GetOnDateRangeByGisCountryId(int gisCountryId, DateTime startDate, DateTime finishDate);
    }
}
