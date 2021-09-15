using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.References;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface ICountryNameService
    {
        Task<IEnumerable<CountryNameDTO>> Getall();
        Task<CountryNameDTO> Get(int id);
        Task<List<CountryNameDTO>> GetAllByCountryId(int id);
        Task<PagingResponse<CountryNameDTO>> GetAllByCountryId(int id, Params parameters);
        Task<CountryNameDTO> Create(CountryNameDTO dto);
        Task<CountryNameDTO> Update(CountryNameDTO dto);
        Task<bool> Delete(int id);
    }
}
