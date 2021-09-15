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
using LogSuite.Client.Serices;

namespace LogSuite.Client.Services
{
    public class FinancialStatementStandardService : IFinancialStatementStandardService
    {
        private readonly HttpClient _client;
        private readonly ToastService _toastService;
        private readonly string _apirUrl = "api/references/financialstatementstandard";

        public FinancialStatementStandardService(HttpClient client, ToastService toastService)
        {
            _client = client;
            _toastService = toastService;
        }
        public async Task<FinancialStatementStandardDTO> Create(FinancialStatementStandardDTO ratingDTO)
        {
            var content = JsonConvert.SerializeObject(ratingDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apirUrl, bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<FinancialStatementStandardDTO>(result);
                return dto;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastrError(errorModel.ErrorMessage);
                return null;
            }
        }

        public async Task<FinancialStatementStandardDTO> Get(int id)
        {
            var response = await _client.GetAsync($"{_apirUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var rating = JsonConvert.DeserializeObject<FinancialStatementStandardDTO>(result);
                return rating;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastrError(errorModel.ErrorMessage);
                return null;
            }
        }

        public async Task<IEnumerable<FinancialStatementStandardDTO>> Getall()
        {
            var response = await _client.GetAsync($"{_apirUrl}");
            var content = await response.Content.ReadAsStringAsync();
            var ratings = JsonConvert.DeserializeObject<IEnumerable<FinancialStatementStandardDTO>>(content);
            return ratings;
        }

        public async Task<FinancialStatementStandardDTO> Update(FinancialStatementStandardDTO fs)
        {
            var content = JsonConvert.SerializeObject(fs);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_apirUrl}/{fs.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<FinancialStatementStandardDTO>(result);
                return dto;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastrError(errorModel.ErrorMessage);
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
                _toastService.ToastrError(errorModel.ErrorMessage);
                return false;
            }
        }
    }
}
