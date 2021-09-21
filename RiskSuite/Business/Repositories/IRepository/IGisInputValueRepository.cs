using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IGisInputValueRepository
    {
        Task<GisInputValueDTO> Create(GisInputValueDTO dto);
        Task<GisInputValueDTO> Update(GisInputValueDTO dto);
        Task<GisInputValueDTO> Get(int id);
        Task<int> Delete(int id);
        Task<GisInputValueDTO> IsUnique(GisInputValueDTO dto, int id = 0);
        Task<PagedList<GisInputValueDTO>> GetPagedByGisId(int gisId, Params parameters);
        Task<GisInputValueDTO> GetOnDateByGisId(int gisId, DateTime date);
        Task<List<GisInputValueDTO>> GetOnDateRangeByGisId(int gisId, DateTime dateStart, DateTime dateEnd);
    }
}
