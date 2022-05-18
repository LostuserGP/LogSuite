using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.IRepository
{
    public interface IGisAddonValueRepository : IRepositoryBase<GisAddonValueDTO>
    {
        Task<PagedList<GisAddonValueDTO>> GetPagedByGisAddonId(int gisAddonId, Params parameters);
        Task<GisAddonValueDTO> GetOnDateByGisAddonId(int gisAddonId, DateTime date);
        Task<List<GisAddonValueDTO>> GetOnDateRangeByGisAddonId(int gisAddonId, DateTime startDate, DateTime finishDate);
    }
}
