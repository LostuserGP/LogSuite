using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Client.Services.DailyReview.References
{
    public interface IGisService
    {
        Task<IEnumerable<GisDTO>> GetAll();
        Task<PagingResponse<GisDTO>> GetAll(Params parameters);
        Task<List<GisDTO>> GetOnDateRange(DateOnly startDate, DateOnly finishDate);
        Task<GisDTO> Get(int id);
        Task<GisDTO> Create(GisDTO dto);
        Task<GisDTO> Update(GisDTO dto);
        Task<bool> Delete(int id);
    }
}
