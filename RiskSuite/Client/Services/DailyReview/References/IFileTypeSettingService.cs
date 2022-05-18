using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.References;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IFileTypeSettingService
    {
        Task<IEnumerable<FileTypeSettingDTO>> Getall();
        Task<PagingResponse<FileTypeSettingDTO>> Getall(Params parameters);
        Task<FileTypeSettingDTO> Get(int id);
        Task<FileTypeSettingDTO> Create(FileTypeSettingDTO dto);
        Task<FileTypeSettingDTO> Update(FileTypeSettingDTO dto);
        Task<bool> Delete(int id);
    }
}
