﻿using System;
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

namespace LogSuite.Client.Services.DailyReview.References;

public class GisCountryAddonService : IGisCountryAddonService
{
    private readonly HttpClient _client;
    private readonly NotificationService _notificationService;
    private const string ApiUrl = "api/giscountryaddon";

    public GisCountryAddonService(HttpClient client, NotificationService notificationService)
    {
        _client = client;
        _notificationService = notificationService;
    }
    
    public async Task<PagingResponse<GisCountryAddonDto>> GetPaged(Params parameters)
    {
        var queryStringParam = StringParser.ParamsToDict(parameters);
        var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{ApiUrl}/paged", queryStringParam));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        var pagingResponse = new PagingResponse<GisCountryAddonDto>
        {
            Items = System.Text.Json.JsonSerializer.Deserialize<List<GisCountryAddonDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
            MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
        };

        return pagingResponse;
    }

    public async Task<IEnumerable<GisCountryAddonDto>> GetAll()
    {
        var response = await _client.GetAsync(ApiUrl);
        var content = await response.Content.ReadAsStringAsync();
        var answers = JsonConvert.DeserializeObject<IEnumerable<GisCountryAddonDto>>(content);
        return answers;
    }

    public async Task<IEnumerable<GisCountryAddonDto>> GetAllByGisCountryId(int gisCountryId)
    {
        var response = await _client.GetAsync($"{ApiUrl}/bygiscountryid/{gisCountryId}");
        var content = await response.Content.ReadAsStringAsync();
        var answers = JsonConvert.DeserializeObject<IEnumerable<GisCountryAddonDto>>(content);
        return answers;
    }
    
    public async Task<IEnumerable<GisCountryAddonDto>> GetAllByGisId(int gisId)
    {
        var response = await _client.GetAsync($"{ApiUrl}/bygisid/{gisId}");
        var content = await response.Content.ReadAsStringAsync();
        var answers = JsonConvert.DeserializeObject<IEnumerable<GisCountryAddonDto>>(content);
        return answers;
    }

    public async Task<PagingResponse<GisCountryAddonDto>> GetAllPagedByGisCountryId(int gisCountryId, Params parameters)
    {
        var queryStringParam = StringParser.ParamsToDict(parameters);
        var response = await _client.GetAsync(QueryHelpers.AddQueryString($"{ApiUrl}/bygiscountryid/{gisCountryId}/paged", queryStringParam));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        var pagingResponse = new PagingResponse<GisCountryAddonDto>
        {
            Items = System.Text.Json.JsonSerializer.Deserialize<List<GisCountryAddonDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
            MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
        };

        return pagingResponse;
    }

    public async Task<GisCountryAddonDto> Get(int id)
    {
        var response = await _client.GetAsync($"{ApiUrl}/{id}");
        var result = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var answer = JsonConvert.DeserializeObject<GisCountryAddonDto>(result);
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

    public async Task<GisCountryAddonDto> Create(GisCountryAddonDto dto)
    {
        var content = JsonConvert.SerializeObject(dto);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(ApiUrl, bodyContent);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<GisCountryAddonDto>(result);
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

    public async Task<GisCountryAddonDto> Update(GisCountryAddonDto dto)
    {
        var content = JsonConvert.SerializeObject(dto);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"{ApiUrl}/{dto.Id}", bodyContent);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<GisCountryAddonDto>(result);
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
}