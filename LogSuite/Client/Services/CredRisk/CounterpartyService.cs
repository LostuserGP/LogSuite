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
using System.Text.Json;
using System.Threading.Tasks;
using LogSuite.Client.Services;
using LogSuite.Shared.Models.CredRisk;
using LogSuite.Shared.Helpers;

namespace LogSuite.Client.Services
{
    public class CounterpartyService : ICounterpartyService
    {
        private readonly HttpClient _client;
        private readonly ToastService _toastService;

        public CounterpartyService(HttpClient client, ToastService toastService)
        {
            _client = client;
            _toastService = toastService;
        }
        public async Task<CounterpartyDTO> Get(int counterpartyId)
        {
            var response = await _client.GetAsync($"api/counterparty/{counterpartyId}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var counterparty = JsonConvert.DeserializeObject<CounterpartyDTO>(result);
                return counterparty;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                _toastService.ToastError(errorModel.ErrorMessage);
                return null;
            }
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
            var queryStringParam = StringParser.ParamsToDict(parameters);
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
