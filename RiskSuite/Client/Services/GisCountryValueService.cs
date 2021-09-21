using LogSuite.Client.Helpers;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
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
    public class GisCountryValueService : IGisCountryValueService
    {
        private readonly HttpClient _client;
        private readonly ToastService _toastService;
        private readonly string _apirUrl = "api/giscountryvalue";

        public GisCountryValueService(HttpClient client, ToastService toastService)
        {
            _client = client;
            _toastService = toastService;
        }

        public async Task<GisCountryValueDTO> Create(GisCountryValueDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apirUrl, bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<GisCountryValueDTO>(result);
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

        public async Task<GisCountryValueDTO> Get(int id)
        {
            var response = await _client.GetAsync($"{_apirUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var answer = JsonConvert.DeserializeObject<GisCountryValueDTO>(result);
                return answer;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastError(errorModel.ErrorMessage);
                return null;
            }
        }

        public async Task<GisCountryValueDTO> GetOnDateByGisCountryId(int gisCountryId, DateTime date)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["date"] = date.ToString()
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{_apirUrl}/bygiscountryidondate/{gisCountryId}", queryStringParam));
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastError(errorModel.ErrorMessage);
                return null;
            }
            var answer = JsonConvert.DeserializeObject<GisCountryValueDTO>(result);
            return answer;
        }

        public async Task<IEnumerable<GisCountryValueDTO>> GetOnDateRangeByGisCountryId(int gisCountryId, DateTime dateStart, DateTime dateEnd)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["dateStart"] = dateStart.ToString(),
                ["dateEnd"] = dateStart.ToString(),
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{_apirUrl}/bygiscountryidondaterange/{gisCountryId}", queryStringParam));
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastError(errorModel.ErrorMessage);
                return null;
            }
            var answers = JsonConvert.DeserializeObject<IEnumerable<GisCountryValueDTO>>(result);
            return answers;
        }

        public async Task<PagingResponse<GisCountryValueDTO>> GetPagedByGisCountryId(int gisCountryId, Params parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString(),
                ["pageSize"] = parameters.PageSize.ToString(),
                ["filter"] = parameters.Filter == null ? "" : parameters.Filter,
                ["order"] = parameters.Order == null ? "" : parameters.Order,
                ["orderAsc"] = parameters.OrderAsc.ToString()
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{_apirUrl}/bygiscountryidpaged/{gisCountryId}", queryStringParam));
            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastError(errorModel.ErrorMessage);
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var pagingResponse = new PagingResponse<GisCountryValueDTO>
            {
                Items = System.Text.Json.JsonSerializer.Deserialize<List<GisCountryValueDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<GisCountryValueDTO> Update(GisCountryValueDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_apirUrl}/{dto.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<GisCountryValueDTO>(result);
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
