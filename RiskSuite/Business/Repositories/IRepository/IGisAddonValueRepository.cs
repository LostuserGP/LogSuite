using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IGisAddonValueRepository
    {
        Task<GisAddonValueDTO> Create(GisAddonValueDTO dto);
        Task<GisAddonValueDTO> Update(GisAddonValueDTO dto);
        Task<GisAddonValueDTO> Get(int id);
        Task<int> Delete(int id);
        Task<GisAddonValueDTO> IsUnique(GisAddonValueDTO dto, int id = 0);
        Task<PagedList<GisAddonValueDTO>> GetPagedByGisAddonId(int gisAddonId, Params parameters);
        Task<GisAddonValueDTO> GetOnDateByGisAddonId(int gisAddonId, DateTime date);
        Task<List<GisAddonValueDTO>> GetOnDateRangeByGisAddonId(int gisAddonId, DateTime dateStart, DateTime dateEnd);
    }
}
