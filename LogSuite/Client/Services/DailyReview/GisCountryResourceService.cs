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
using LogSuite.Shared.Models.DailyReview;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Radzen;

namespace LogSuite.Client.Services.DailyReview
{
    public class GisCountryResourceService : IGisCountryResourceService
    {
        private readonly HttpClient _client;
        private readonly NotificationService _notificationService;
        private const string ApiUrl = "api/giscountryresource";

        public GisCountryResourceService(HttpClient client, NotificationService notificationService)
        {
            _client = client;
            _notificationService = notificationService;
        }

        public async Task<GisCountryResourceDTO> Create(GisCountryResourceDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(ApiUrl, bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<GisCountryResourceDTO>(result);
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
            else
            {
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

        public async Task<GisCountryResourceDTO> Get(int id)
        {
            var response = await _client.GetAsync($"{ApiUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var answer = JsonConvert.DeserializeObject<GisCountryResourceDTO>(result);
                return answer;
            }
            else
            {
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

        public async Task<GisCountryResourceDTO> GetOnDateByGisCountryId(int gisCountryId, DateOnly date)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["date"] = date.ToString()
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{ApiUrl}/bygiscountryidondate/{gisCountryId}", queryStringParam));
            var result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
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
            var answer = JsonConvert.DeserializeObject<GisCountryResourceDTO>(result);
            return answer;
        }

        public async Task<PagingResponse<GisCountryResourceDTO>> GetPagedByGisCountryId(int gisCountryId, Params parameters)
        {
            var queryStringParam = StringParser.ParamsToDict(parameters);
            var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{ApiUrl}/bygiscountryidpaged/{gisCountryId}", queryStringParam));
            if (!response.IsSuccessStatusCode)
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
            var content = await response.Content.ReadAsStringAsync();
            var pagingResponse = new PagingResponse<GisCountryResourceDTO>
            {
                Items = System.Text.Json.JsonSerializer.Deserialize<List<GisCountryResourceDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<GisCountryResourceDTO> Update(GisCountryResourceDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{ApiUrl}/{dto.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<GisCountryResourceDTO>(result);
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
    }
}
