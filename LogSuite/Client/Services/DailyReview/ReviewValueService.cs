using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Newtonsoft.Json;
using Radzen;

namespace LogSuite.Client.Services.DailyReview
{
    public class ReviewValueService : IReviewValueService
    {
        private readonly HttpClient _client;
        private readonly NotificationService _notificationService;
        private const string ApiUrl = "api/reviewinputvalue";

        public ReviewValueService(HttpClient client, NotificationService notificationService)
        {
            _client = client;
            _notificationService = notificationService;
        }

        public async Task<int[]> CreateOrUpdate(ReviewValueList dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(ApiUrl, bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<int[]>(result);
                return answer;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                if (errorModel != null)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Summary = errorModel.Title,
                        Detail = errorModel.ErrorMessage,
                        Duration = 3000
                    });
                }
                return null;
            }
        }

    }
}
