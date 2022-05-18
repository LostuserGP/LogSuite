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
    public class RatingExternalService : IRatingExternalService
    {
        private readonly HttpClient _client;

        public RatingExternalService(HttpClient client)
        {
            _client = client;
        }
        public async Task<RatingExternalDTO> Create(RatingExternalDTO ratingDTO)
        {
            var content = JsonConvert.SerializeObject(ratingDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/ratingexternal", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<RatingExternalDTO>(result);
                return dto;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<RatingExternalDTO> Get(int ratingId)
        {
            var response = await _client.GetAsync($"api/ratingexternal/{ratingId}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var rating = JsonConvert.DeserializeObject<RatingExternalDTO>(result);
                return rating;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<RatingExternalDTO>> Getall(int counterpartyId)
        {
            var response = await _client.GetAsync($"api/ratingexternal/all/{counterpartyId}");
            var content = await response.Content.ReadAsStringAsync();
            var ratings = JsonConvert.DeserializeObject<IEnumerable<RatingExternalDTO>>(content);
            return ratings;
        }

        public async Task<RatingExternalDTO> Update(RatingExternalDTO ratingDTO)
        {
            var content = JsonConvert.SerializeObject(ratingDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"api/ratingexternal/{ratingDTO.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<RatingExternalDTO>(result);
                return dto;
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
            var response = await _client.DeleteAsync($"api/ratingexternal/{id}");
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
