using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models.Operativka;
using System;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IGisOutputValueRepository
    {
        Task<GisOutputValueDTO> Create(GisOutputValueDTO dto);
        Task<GisOutputValueDTO> Update(GisOutputValueDTO dto);
        Task<GisOutputValueDTO> Get(int id);
        Task<int> Delete(int id);
        Task<GisOutputValueDTO> IsUnique(GisOutputValueDTO dto, int id = 0);
        Task<PagedList<GisOutputValueDTO>> GetByGisId(int gisId, Params parameters);
        Task<GisOutputValueDTO> GetOnDateByGisId(int gisId, DateTime date);
        Task<PagedList<GisOutputValueDTO>> GetOnDateRangeByGisId(int gisId, DateTime dateStart, DateTime dateEnd, Params parameters);
    }
}
