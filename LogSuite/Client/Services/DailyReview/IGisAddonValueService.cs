using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Client.Services.DailyReview
{
    public interface IGisAddonValueService
    {
        Task<PagingResponse<GisAddonValueDTO>> GetPagedByGisAddonId(int gisAddonId, Params parameters);
        Task<GisAddonValueDTO> GetOnDateByGisAddonId(int gisAddonId, DateOnly date);
        Task<IEnumerable<GisAddonValueDTO>> GetOnDateRangeByGisAddonId(int gisAddonId, DateOnly dateStart, DateOnly dateEnd);
        Task<GisAddonValueDTO> Get(int id);
        Task<GisAddonValueDTO> Create(GisAddonValueDTO dto);
        Task<GisAddonValueDTO> Update(GisAddonValueDTO dto);
        Task<bool> Delete(int id);
    }
}
