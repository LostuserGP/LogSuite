using System.Threading.Tasks;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Business.Repositories.DailyReview
{
    public interface IInputFileLogRepository : IRepositoryValueBase<InputFileLogDTO>
    {
        Task<InputFileLogDTO> GetByFilename(string filename);
    }
}
