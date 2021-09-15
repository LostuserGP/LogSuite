using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using LogSuite.Client.Helpers;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Authorization;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LogSuite.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;

        public AccountService(HttpClient client)
        {
            _client = client;
        }
        public async Task<UserDetailDTO> Create(UserDetailDTO accountDTO)
        {
            var content = JsonConvert.SerializeObject(accountDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/create", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var account = JsonConvert.DeserializeObject<UserDetailDTO>(result);
                return account;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<UserDetailDTO> Get(string accountId)
        {
            var response = await _client.GetAsync($"api/account/get/{accountId}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var account = JsonConvert.DeserializeObject<UserDetailDTO>(result);
                return account;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<UserDetailDTO>> Getall()
        {
            var response = await _client.GetAsync($"api/account");
            var content = await response.Content.ReadAsStringAsync();
            var accounts = JsonConvert.DeserializeObject<IEnumerable<UserDetailDTO>>(content);
            return accounts;
        }

        public async Task<PagingResponse<UserDetailDTO>> Getall(Params parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString(),
                ["pageSize"] = parameters.PageSize.ToString(),
                ["filter"] = parameters.Filter == null ? "" : parameters.Filter,
                ["order"] = parameters.Order == null ? "" : parameters.Order,
                ["orderAsc"] = parameters.OrderAsc.ToString()
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString("api/account/getusers", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<UserDetailDTO>
            {
                Items = System.Text.Json.JsonSerializer.Deserialize<List<UserDetailDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<UserDetailDTO> Update(UserDetailDTO accountDTO)
        {
            var content = JsonConvert.SerializeObject(accountDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"api/account/update/{accountDTO.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var account = JsonConvert.DeserializeObject<UserDetailDTO>(result);
                return account;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }
    
        public async Task<List<string>> GetRoles()
        {
            var response = await _client.GetAsync($"api/account/roles");
            var content = await response.Content.ReadAsStringAsync();
            var roles = JsonConvert.DeserializeObject<List<string>>(content);
            return roles;
        }
    }
}
