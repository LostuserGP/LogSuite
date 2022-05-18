using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IGisOutputValueService
    {
        Task<PagingResponse<GisOutputValueDTO>> GetPagedByGisId(int gisId, Params parameters);
        Task<GisOutputValueDTO> GetOnDateByGisId(int gisId, DateTime date);
        Task<IEnumerable<GisOutputValueDTO>> GetOnDateRangeByGisId(int gisId, DateTime dateStart, DateTime dateEnd);
        Task<GisOutputValueDTO> Get(int id);
        Task<GisOutputValueDTO> Create(GisOutputValueDTO dto);
        Task<GisOutputValueDTO> Update(GisOutputValueDTO dto);
        Task<bool> Delete(int id);
    }
}
