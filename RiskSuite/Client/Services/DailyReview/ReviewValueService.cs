using LogSuite.Client.Helpers;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogSuite.Client.Services
{
    public class ReviewValueService : IReviewValueService
    {
        private readonly HttpClient _client;
        private readonly ToastService _toastService;
        private readonly string _apirUrl = "api/reviewinputvalue";

        public ReviewValueService(HttpClient client, ToastService toastService)
        {
            _client = client;
            _toastService = toastService;
        }

        public async Task<int[]> CreateOrUpdate(ReviewValueList dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apirUrl, bodyContent);
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
                _toastService.ToastError(errorModel.ErrorMessage);
                return null;
            }
        }

    }
}
