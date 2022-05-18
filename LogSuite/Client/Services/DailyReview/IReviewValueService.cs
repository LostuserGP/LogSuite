using System.Threading.Tasks;
using LogSuite.Shared.Models.DailyReview;

namespace LogSuite.Client.Services.DailyReview
{
    public interface IReviewValueService
    {
        Task<int[]> CreateOrUpdate(ReviewValueList dto);
    }
}
