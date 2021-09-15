using LogSuite.Client.Helpers;
using LogSuite.Client.Serices;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
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
    public class CountryNameService : ICountryNameService
    {
        private readonly HttpClient _client;
        private readonly ToastService _toastService;
        private readonly string _apirUrl = "api/references/countryname";

        public CountryNameService(HttpClient client, ToastService toastService)
        {
            _client = client;
            _toastService = toastService;
        }

        public async Task<CountryNameDTO> Create(CountryNameDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apirUrl, bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<CountryNameDTO>(result);
                return answer;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastrError(errorModel.ErrorMessage);
                return null;
            }
        }

        public async Task<CountryNameDTO> Get(int id)
        {
            var response = await _client.GetAsync($"{_apirUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var answer = JsonConvert.DeserializeObject<CountryNameDTO>(result);
                return answer;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastrError(errorModel.ErrorMessage);
                return null;
            }
        }

        public async Task<IEnumerable<CountryNameDTO>> Getall()
        {
            var response = await _client.GetAsync(_apirUrl);
            var content = await response.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<IEnumerable<CountryNameDTO>>(content);
            return answers;
        }

        public async Task<PagingResponse<CountryNameDTO>> GetAllByCountryId(int countryId, Params parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString(),
                ["pageSize"] = parameters.PageSize.ToString(),
                ["filter"] = parameters.Filter == null ? "" : parameters.Filter,
                ["order"] = parameters.Order == null ? "" : parameters.Order,
                ["orderAsc"] = parameters.OrderAsc.ToString()
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{_apirUrl}/paged/{countryId}", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<CountryNameDTO>
            {
                Items = System.Text.Json.JsonSerializer.Deserialize<List<CountryNameDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<CountryNameDTO> Update(CountryNameDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_apirUrl}/{dto.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<CountryNameDTO>(result);
                return answer;
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

        public async Task<List<CountryNameDTO>> GetAllByCountryId(int countryId)
        {
            var response = await _client.GetAsync($"{_apirUrl}/all/{countryId}");
            var content = await response.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<List<CountryNameDTO>>(content);
            return answers;
        }
    }
}
