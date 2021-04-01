using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using RiskSuite.Client.Helpers;
using RiskSuite.Client.Services.IServices;
using RiskSuite.Shared;
using RiskSuite.Shared.Authorization;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RiskSuite.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;

        public AccountService(HttpClient client)
        {
            _client = client;
        }
        public async Task<UserDTO> Create(UserDTO accountDTO)
        {
            var content = JsonConvert.SerializeObject(accountDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var account = JsonConvert.DeserializeObject<UserDTO>(result);
                return account;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<UserDTO> Get(int accountId)
        {
            var response = await _client.GetAsync($"api/account/{accountId}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var account = JsonConvert.DeserializeObject<UserDTO>(result);
                return account;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<UserDTO>> Getall()
        {
            var response = await _client.GetAsync($"api/account");
            var content = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<IEnumerable<UserDTO>>(content);
            return accounts;
        }

        public async Task<PagingResponse<UserDTO>> Getall(Params parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString(),
                ["pageSize"] = parameters.PageSize.ToString(),
                ["filter"] = parameters.Filter == null ? "" : parameters.Filter,
                ["order"] = parameters.Order == null ? "" : parameters.Order,
                ["orderAsc"] = parameters.OrderAsc.ToString()
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString("api/account", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<UserDTO>
            {
                Items = System.Text.Json.JsonSerializer.Deserialize<List<UserDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<UserDTO> Update(UserDTO accountDTO)
        {
            var content = JsonConvert.SerializeObject(accountDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"api/account/{accountDTO.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var account = JsonConvert.DeserializeObject<UserDTO>(result);
                return account;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    }
}
