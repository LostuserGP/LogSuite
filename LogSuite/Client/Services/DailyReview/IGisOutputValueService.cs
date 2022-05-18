using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Client.Services.DailyReview
{
    public interface IGisOutputValueService
    {
        Task<PagingResponse<GisOutputValueDTO>> GetPagedByGisId(int gisId, Params parameters);
        Task<GisOutputValueDTO> GetOnDateByGisId(int gisId, DateOnly date);
        Task<IEnumerable<GisOutputValueDTO>> GetOnDateRangeByGisId(int gisId, DateOnly dateStart, DateOnly dateEnd);
        Task<GisOutputValueDTO> Get(int id);
        Task<GisOutputValueDTO> Create(GisOutputValueDTO dto);
        Task<GisOutputValueDTO> Update(GisOutputValueDTO dto);
        Task<bool> Delete(int id);
    }
}
