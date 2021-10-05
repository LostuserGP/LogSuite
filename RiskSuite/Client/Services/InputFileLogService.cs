using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace LogSuite.Client.Services
{
    public class InputFileLogService : IInputFileLogService
    {
        private readonly HttpClient _client;
        private readonly ToastService _toastService;
        private readonly string _apirUrl = "api/inputfilelog";

        public InputFileLogService(HttpClient client, ToastService toastService)
        {
            _client = client;
            _toastService = toastService;
        }

        public async Task<InputFileLogDTO> Get(int id)
        {
            var response = await _client.GetAsync($"{_apirUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var answer = JsonConvert.DeserializeObject<InputFileLogDTO>(result);
                return answer;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastError(errorModel.ErrorMessage);
                return null;
            }
        }
    }
}
