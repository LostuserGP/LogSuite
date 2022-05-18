using LogSuite.Shared.Models.DailyReview;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IInputFileLogService
    {
        Task<InputFileLogDTO> Get(int id);
    }
}
