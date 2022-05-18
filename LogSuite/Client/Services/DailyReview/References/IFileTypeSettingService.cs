using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.References;

namespace LogSuite.Client.Services.DailyReview.References
{
    public interface IFileTypeSettingService
    {
        Task<IEnumerable<FileTypeSettingDTO>> GetAll();
        Task<PagingResponse<FileTypeSettingDTO>> GetAll(Params parameters);
        Task<FileTypeSettingDTO> Get(int id);
        Task<FileTypeSettingDTO> Create(FileTypeSettingDTO dto);
        Task<FileTypeSettingDTO> Update(FileTypeSettingDTO dto);
        Task<bool> Delete(int id);
    }
}
