using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
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
using LogSuite.Shared.Helpers;
using LogSuite.Client.Services.IServices;
using LogSuite.Client.Services;

namespace LogSuite.Client.Services
{
    public class GisCountryService : IGisCountryService
    {
        private readonly HttpClient _client;
        private readonly ToastService _toastService;
        private readonly string _apirUrl = "api/giscountry";

        public GisCountryService(HttpClient client, ToastService toastService)
        {
            _client = client;
            _toastService = toastService;
        }

        public async Task<GisCountryDTO> Create(GisCountryDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apirUrl, bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<GisCountryDTO>(result);
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

        public async Task<GisCountryDTO> Get(int id)
        {
            var response = await _client.GetAsync($"{_apirUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var answer = JsonConvert.DeserializeObject<GisCountryDTO>(result);
                return answer;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastError(errorModel.ErrorMessage);
                return null;
            }
        }

        public async Task<IEnumerable<GisCountryDTO>> GetAll()
        {
            var response = await _client.GetAsync(_apirUrl);
            var content = await response.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<IEnumerable<GisCountryDTO>>(content);
            return answers;
        }
        
        public async Task<IEnumerable<GisCountryDTO>> GetAllByCountryId(int countryId)
        {
            var response = await _client.GetAsync($"{_apirUrl}/bycountryid/{countryId}");
            var content = await response.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<IEnumerable<GisCountryDTO>>(content);
            return answers;
        }

        public async Task<IEnumerable<GisCountryDTO>> GetAllByGisId(int gisId)
        {
            var response = await _client.GetAsync($"{_apirUrl}/bygisid/{gisId}");
            var content = await response.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<IEnumerable<GisCountryDTO>>(content);
            return answers;
        }

        public async Task<PagingResponse<GisCountryDTO>> GetAllPaged(Params parameters)
        {
            var queryStringParam = StringParser.ParamsToDict(parameters);
            var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{_apirUrl}/paged", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<GisCountryDTO>
            {
                Items = System.Text.Json.JsonSerializer.Deserialize<List<GisCountryDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<PagingResponse<GisCountryDTO>> GetAllPagedByCountryId(int countryId, Params parameters)
        {
            var queryStringParam = StringParser.ParamsToDict(parameters);
            var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{_apirUrl}/bycountryid/{countryId}/paged", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<GisCountryDTO>
            {
                Items = System.Text.Json.JsonSerializer.Deserialize<List<GisCountryDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<PagingResponse<GisCountryDTO>> GetAllPagedByGisId(int gisId, Params parameters)
        {
            var queryStringParam = StringParser.ParamsToDict(parameters);
            var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{_apirUrl}/bygisid/{gisId}/paged", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<GisCountryDTO>
            {
                Items = System.Text.Json.JsonSerializer.Deserialize<List<GisCountryDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<GisCountryDTO> Update(GisCountryDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_apirUrl}/{dto.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<GisCountryDTO>(result);
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
