using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IGisInputValueService
    {
        Task<PagingResponse<GisInputValueDTO>> GetPagedByGisId(int gisId, Params parameters);
        Task<GisInputValueDTO> GetOnDateByGisId(int gisId, DateTime date);
        Task<IEnumerable<GisInputValueDTO>> GetOnDateRangeByGisId(int gisId, DateTime dateStart, DateTime dateEnd);
        Task<GisInputValueDTO> Get(int id);
        Task<GisInputValueDTO> Create(GisInputValueDTO dto);
        Task<GisInputValueDTO> Update(GisInputValueDTO dto);
        Task<bool> Delete(int id);
    }
}
