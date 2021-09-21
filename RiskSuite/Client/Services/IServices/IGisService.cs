using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IGisService
    {
        Task<IEnumerable<GisDTO>> Getall();
        Task<PagingResponse<GisDTO>> Getall(Params parameters);
        Task<GisDTO> Get(int id);
        Task<GisDTO> Create(GisDTO dto);
        Task<GisDTO> Update(GisDTO dto);
        Task<bool> Delete(int id);
    }
}
