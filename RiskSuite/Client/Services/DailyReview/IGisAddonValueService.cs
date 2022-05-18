using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IGisAddonValueService
    {
        Task<PagingResponse<GisAddonValueDTO>> GetPagedByGisAddonId(int gisAddonId, Params parameters);
        Task<GisAddonValueDTO> GetOnDateByGisAddonId(int gisAddonId, DateTime date);
        Task<IEnumerable<GisAddonValueDTO>> GetOnDateRangeByGisAddonId(int gisAddonId, DateTime dateStart, DateTime dateEnd);
        Task<GisAddonValueDTO> Get(int id);
        Task<GisAddonValueDTO> Create(GisAddonValueDTO dto);
        Task<GisAddonValueDTO> Update(GisAddonValueDTO dto);
        Task<bool> Delete(int id);
    }
}
