using LogSuite.Client.Helpers;
using LogSuite.Client.Serices;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Helpers;
using LogSuite.Shared.Models.Operativka;
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
    public class GisAddonService : IGisAddonService
    {
        private readonly HttpClient _client;
        private readonly ToastService _toastService;
        private readonly string _apirUrl = "api/gisaddon";

        public GisAddonService(HttpClient client, ToastService toastService)
        {
            _client = client;
            _toastService = toastService;
        }

        public async Task<GisAddonDTO> Create(GisAddonDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apirUrl, bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<GisAddonDTO>(result);
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

        public async Task<GisAddonDTO> Get(int id)
        {
            var response = await _client.GetAsync($"{_apirUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var answer = JsonConvert.DeserializeObject<GisAddonDTO>(result);
                return answer;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastrError(errorModel.ErrorMessage);
                return null;
            }
        }

        public async Task<IEnumerable<GisAddonDTO>> GetAll()
        {
            var response = await _client.GetAsync(_apirUrl);
            var content = await response.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<IEnumerable<GisAddonDTO>>(content);
            return answers;
        }

        public async Task<IEnumerable<GisAddonDTO>> GetAllByGisId(int gisId)
        {
            var response = await _client.GetAsync($"{_apirUrl}/bygisid/{gisId}");
            var content = await response.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<IEnumerable<GisAddonDTO>>(content);
            return answers;
        }

        public async Task<PagingResponse<GisAddonDTO>> GetPaged(Params parameters)
        {
            var queryStringParam = StringParser.ParamsToDict(parameters);
            var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{_apirUrl}/paged", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<GisAddonDTO>
            {
                Items = System.Text.Json.JsonSerializer.Deserialize<List<GisAddonDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<PagingResponse<GisAddonDTO>> GetPagedByGisId(int gisId, Params parameters)
        {
            var queryStringParam = StringParser.ParamsToDict(parameters);
            var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{_apirUrl}/bygisid/{gisId}/paged", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<GisAddonDTO>
            {
                Items = System.Text.Json.JsonSerializer.Deserialize<List<GisAddonDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<GisAddonDTO> Update(GisAddonDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_apirUrl}/{dto.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<GisAddonDTO>(result);
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
    }
}
