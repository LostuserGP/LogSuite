using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Helpers;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Radzen;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace LogSuite.Client.Services.References
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _client;
        private readonly NotificationService _notificationService;
        private const string ApiUrl = "api/references/country";

        public CountryService(HttpClient client, NotificationService notificationService)
        {
            _client = client;
            _notificationService = notificationService;
        }

        public async Task<CountryDTO> Create(CountryDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(ApiUrl, bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<CountryDTO>(result);
                return answer;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                if (errorModel != null)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Summary = errorModel.Title,
                        Detail = errorModel.ErrorMessage,
                        Duration = 3000
                    });
                }

                return null;
            }
        }

        public async Task<CountryDTO> Get(int id)
        {
            var response = await _client.GetAsync($"{ApiUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var answer = JsonConvert.DeserializeObject<CountryDTO>(result);
                return answer;
            }

            var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
            if (errorModel != null)
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = errorModel.Title,
                    Detail = errorModel.ErrorMessage,
                    Duration = 3000
                });
            }

            return null;
        }

        public async Task<IEnumerable<CountryDTO>> GetAll()
        {
            var response = await _client.GetAsync(ApiUrl);
            var content = await response.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<IEnumerable<CountryDTO>>(content);
            return answers;
        }

        public async Task<PagingResponse<CountryDTO>> GetAll(Params parameters)
        {
            var queryStringParam = StringParser.ParamsToDict(parameters);
            var response = await _client.GetAsync(QueryHelpers.AddQueryString(ApiUrl, queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<CountryDTO>
            {
                Items = JsonSerializer.Deserialize<List<CountryDTO>>(content,
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true}),
                MetaData = JsonSerializer.Deserialize<MetaData>(
                    response.Headers.GetValues("X-Pagination").First(),
                    new JsonSerializerOptions {PropertyNameCaseInsensitive = true})
            };

            return pagingResponse;
        }

        public async Task<CountryDTO> Update(CountryDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{ApiUrl}/{dto.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<CountryDTO>(result);
                return answer;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                if (errorModel != null)
                {
                    _notificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Summary = errorModel.Title,
                        Detail = errorModel.ErrorMessage,
                        Duration = 3000
                    });
                }

                return null;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _client.DeleteAsync($"{ApiUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
            if (errorModel != null)
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = errorModel.Title,
                    Detail = errorModel.ErrorMessage,
                    Duration = 3000
                });
            }

            return false;
        }
    }
}