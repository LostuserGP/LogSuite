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
    public class FinancialStatementService : IFinancialStatementService
    {
        private readonly HttpClient _client;
        private readonly ToastService _toastService;
        private readonly string _apirUrl = "api/financialstatement";

        public FinancialStatementService(HttpClient client, ToastService toastService)
        {
            _client = client;
            _toastService = toastService;
        }
        public async Task<FinancialStatementDTO> Create(FinancialStatementDTO ratingDTO)
        {
            var content = JsonConvert.SerializeObject(ratingDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apirUrl, bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<FinancialStatementDTO>(result);
                return dto;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastError(errorModel.ErrorMessage);
                return null;
            }
        }

        public async Task<FinancialStatementDTO> Get(int id)
        {
            var response = await _client.GetAsync($"{_apirUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var rating = JsonConvert.DeserializeObject<FinancialStatementDTO>(result);
                return rating;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastError(errorModel.ErrorMessage);
                return null;
            }
        }

        public async Task<IEnumerable<FinancialStatementDTO>> Getall(int counterpartyId)
        {
            var response = await _client.GetAsync($"{_apirUrl}/all/{counterpartyId}");
            var content = await response.Content.ReadAsStringAsync();
            var ratings = JsonConvert.DeserializeObject<IEnumerable<FinancialStatementDTO>>(content);
            return ratings;
        }

        public async Task<FinancialStatementDTO> Update(FinancialStatementDTO fs)
        {
            var content = JsonConvert.SerializeObject(fs);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_apirUrl}/{fs.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<FinancialStatementDTO>(result);
                return dto;
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
