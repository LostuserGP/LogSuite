using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using LogSuite.Client.Helpers;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LogSuite.Client.Services;
using LogSuite.Shared.Models.CredRisk;

namespace LogSuite.Client.Services
{
    public class RatingService : IRatingService
    {
        private readonly HttpClient _client;
        private readonly ToastService _toastService;
        private readonly string _apirUrl = "api/references/rating";

        public RatingService(HttpClient client, ToastService toastService)
        {
            _client = client;
            _toastService = toastService;
        }

        public async Task<RatingDTO> Create(RatingDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apirUrl, bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<RatingDTO>(result);
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

        public async Task<RatingDTO> Get(int id)
        {
            var response = await _client.GetAsync($"{_apirUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var answer = JsonConvert.DeserializeObject<RatingDTO>(result);
                return answer;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastError(errorModel.ErrorMessage);
                return null;
                //throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<RatingDTO>> Getall()
        {
            var response = await _client.GetAsync(_apirUrl);
            var content = await response.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<IEnumerable<RatingDTO>>(content);
            return answers;
        }

        public async Task<RatingDTO> Update(RatingDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_apirUrl}/{dto.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<RatingDTO>(result);
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

        public async Task<bool> Delete(int id)
        {
            var response = await _client.DeleteAsync($"{_apirUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastError(errorModel.ErrorMessage);
                return false;
            }
        }
    }
}
