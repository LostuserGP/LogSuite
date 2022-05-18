using System.Threading.Tasks;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.IRepository
{
    public interface IInputFileLogRepository : IRepositoryBase<InputFileLogDTO>
    {
        Task<InputFileLogDTO> GetByFilename(string filename);
    }
}
