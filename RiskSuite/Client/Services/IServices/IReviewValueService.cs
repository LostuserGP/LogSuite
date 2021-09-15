using LogSuite.Shared.Models;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IReviewValueService
    {
        Task<int[]> CreateOrUpdate(ReviewValueListDTO dto);
    }
}
