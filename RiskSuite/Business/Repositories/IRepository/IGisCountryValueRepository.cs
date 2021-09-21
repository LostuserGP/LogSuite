using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IGisCountryValueRepository
    {
        Task<GisCountryValueDTO> Create(GisCountryValueDTO dto);
        Task<GisCountryValueDTO> Update(GisCountryValueDTO dto);
        Task<GisCountryValueDTO> Get(int id);
        Task<int> Delete(int id);
        Task<GisCountryValueDTO> IsUnique(GisCountryValueDTO dto, int id = 0);
        Task<PagedList<GisCountryValueDTO>> GetPagedByGisCountryId(int gisCountryId, Params parameters);
        Task<GisCountryValueDTO> GetOnDateByGisCountryId(int gisCountryId, DateTime date);
        Task<IEnumerable<GisCountryValueDTO>> GetOnDateRangeByGisCountryId(int gisCountryId, DateTime dateStart, DateTime dateEnd);
    }
}
