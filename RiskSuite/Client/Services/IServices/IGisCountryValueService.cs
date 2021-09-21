using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IGisCountryValueService
    {
        Task<PagingResponse<GisCountryValueDTO>> GetPagedByGisCountryId(int gisCountryId, Params parameters);
        Task<GisCountryValueDTO> GetOnDateByGisCountryId(int gisCountryId, DateTime date);
        Task<IEnumerable<GisCountryValueDTO>> GetOnDateRangeByGisCountryId(int gisCountryId, DateTime dateStart, DateTime dateEnd);
        Task<GisCountryValueDTO> Get(int id);
        Task<GisCountryValueDTO> Create(GisCountryValueDTO dto);
        Task<GisCountryValueDTO> Update(GisCountryValueDTO dto);
        Task<bool> Delete(int id);
    }
}
