using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models.References;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository.References
{
    public interface IFileTypeSettingRepository
    {
        Task<FileTypeSettingDTO> Create(FileTypeSettingDTO dto);
        Task<FileTypeSettingDTO> Update(FileTypeSettingDTO dto);
        Task<FileTypeSettingDTO> Get(int id);
        Task<int> Delete(int id);
        Task<IEnumerable<FileTypeSettingDTO>> GetAll();
        Task<PagedList<FileTypeSettingDTO>> GetPaged(Params parameters);
    }
}
