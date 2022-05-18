using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Client.Services.DailyReview
{
    public interface IGisInputValueService
    {
        Task<PagingResponse<GisInputValueDTO>> GetPagedByGisId(int gisId, Params parameters);
        Task<GisInputValueDTO> GetOnDateByGisId(int gisId, DateOnly date);
        Task<IEnumerable<GisInputValueDTO>> GetOnDateRangeByGisId(int gisId, DateOnly dateStart, DateOnly dateEnd);
        Task<GisInputValueDTO> Get(long id);
        Task<GisInputValueDTO> Create(GisInputValueDTO dto);
        Task<GisInputValueDTO> Update(GisInputValueDTO dto);
        Task<bool> Delete(long id);
    }
}
