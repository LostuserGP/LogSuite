using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using RiskSuite.Client.Helpers;
using RiskSuite.Client.Services.IServices;
using RiskSuite.Shared;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RiskSuite.Client.Services
{
    public class CounterpartyService : ICounterpartyService
    {
        private readonly HttpClient _client;

        public CounterpartyService(HttpClient client)
        {
            _client = client;
        }
        public Task<CounterpartyDTO> Get(int counterpartyId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CounterpartyDTO>> Getall()
        {
            var response = await _client.GetAsync($"api/counterparty");
            var content = await response.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<IEnumerable<CounterpartyDTO>>(content);
            return rooms;
        }

        public async Task<PagingResponse<CounterpartyDTO>> Getall(Params parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString(),
                ["pageSize"] = parameters.PageSize.ToString(),
                ["filter"] = parameters.Filter == null ? "" : parameters.Filter,
                ["order"] = parameters.Order == null ? "" : parameters.Order,
                ["orderAsc"] = parameters.OrderAsc.ToString()
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString("api/counterparty", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<CounterpartyDTO>
            {
                Items = System.Text.Json.JsonSerializer.Deserialize<List<CounterpartyDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }
    }
}
