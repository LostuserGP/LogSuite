using Microsoft.AspNetCore.WebUtilities;
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
using LogSuite.Shared.Models.CredRisk;

namespace LogSuite.Client.Services
{
    public class CommitteeLimitService : ICommitteeLimitService
    {
        private readonly HttpClient _client;
        private readonly string apiUrl = "api/references/committeelimit";

        public CommitteeLimitService(HttpClient client)
        {
            _client = client;
        }

        public async Task<CommitteeLimitDTO> Create(CommitteeLimitDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(apiUrl, bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var committeeLimit = JsonConvert.DeserializeObject<CommitteeLimitDTO>(result);
                return committeeLimit;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<CommitteeLimitDTO> Get(int id)
        {
            var response = await _client.GetAsync($"{apiUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var answer = JsonConvert.DeserializeObject<CommitteeLimitDTO>(result);
                return answer;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<CommitteeLimitDTO>> Getall()
        {
            var response = await _client.GetAsync(apiUrl);
            var content = await response.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<IEnumerable<CommitteeLimitDTO>>(content);
            return answers;
        }

        public async Task<PagingResponse<CommitteeLimitDTO>> Getall(Params parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString(),
                ["pageSize"] = parameters.PageSize.ToString(),
                ["filter"] = parameters.Filter == null ? "" : parameters.Filter,
                ["order"] = parameters.Order == null ? "" : parameters.Order,
                ["orderAsc"] = parameters.OrderAsc.ToString()
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString(apiUrl, queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<CommitteeLimitDTO>
            {
                Items = System.Text.Json.JsonSerializer.Deserialize<List<CommitteeLimitDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<CommitteeLimitDTO> Update(CommitteeLimitDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{apiUrl}/{dto.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<CommitteeLimitDTO>(result);
                return answer;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _client.DeleteAsync($"{apiUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
