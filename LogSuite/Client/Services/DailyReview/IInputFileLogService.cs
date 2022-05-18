using System.Threading.Tasks;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Client.Services.DailyReview
{
    public interface IInputFileLogService
    {
        Task<InputFileLogDTO> Get(long id);
    }
}
