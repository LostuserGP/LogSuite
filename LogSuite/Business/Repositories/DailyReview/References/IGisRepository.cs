using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview.References
{
    public interface IGisRepository : IRepositoryBase<GisDTO>
    {
        Task<List<GisDTO>> GetOnDateRange(DateOnly startDate, DateOnly finishDate);
        
    }
}