using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using LogSuite.Shared.Models.References;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IGisCountryService
    {
        Task<PagingResponse<GisCountryDTO>> GetAllPaged(Params parameters);
        Task<IEnumerable<GisCountryDTO>> GetAll();
        Task<IEnumerable<GisCountryDTO>> GetAllByGisId(int gisId);
        Task<PagingResponse<GisCountryDTO>> GetAllPagedByGisId(int gisId, Params parameters);
        Task<IEnumerable<GisCountryDTO>> GetAllByCountryId(int countryId);
        Task<PagingResponse<GisCountryDTO>> GetAllPagedByCountryId(int countryId, Params parameters);
        Task<GisCountryDTO> Get(int id);
        Task<GisCountryDTO> Create(GisCountryDTO dto);
        Task<GisCountryDTO> Update(GisCountryDTO dto);
        Task<bool> Delete(int id);
    }
}
