using System.Net.Http;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Newtonsoft.Json;
using Radzen;

namespace LogSuite.Client.Services.DailyReview
{
    public class InputFileLogService : IInputFileLogService
    {
        private readonly HttpClient _client;
        private readonly NotificationService _notificationService;
        private const string ApiUrl = "api/inputfilelog";

        public InputFileLogService(HttpClient client, NotificationService notificationService)
        {
            _client = client;
            _notificationService = notificationService;
        }

        public async Task<InputFileLogDTO> Get(long id)
        {
            var response = await _client.GetAsync($"{ApiUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var answer = JsonConvert.DeserializeObject<InputFileLogDTO>(result);
                return answer;
            }
            else
            {
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
