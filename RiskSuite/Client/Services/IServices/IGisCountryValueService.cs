using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.Operativka;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IGisCountryValueService
    {
        Task<IEnumerable<GisCountryValueDTO>> GetAllByGisCountryId(int gisCountryId);
        Task<PagingResponse<GisCountryValueDTO>> GetPagedByGisCountryId(int gisCountryId, Params parameters);
        Task<GisCountryValueDTO> GetOnDateByGisCountryId(int gisCountryId, DateTime date);
        Task<IEnumerable<GisCountryValueDTO>> GetOnDateRangeByGisCountryId(int gisCountryId, DateTime dateStart, DateTime dateEnd);
        Task<GisCountryDTO> Get(int id);
        Task<GisCountryDTO> Create(GisCountryValueDTO dto);
        Task<GisCountryDTO> Update(GisCountryValueDTO dto);
        Task<bool> Delete(int id);
    }
}
