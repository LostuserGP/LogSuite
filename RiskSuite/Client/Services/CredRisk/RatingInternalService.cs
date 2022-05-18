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
    public class RatingInternalService : IRatingInternalService
    {
        private readonly HttpClient _client;

        public RatingInternalService(HttpClient client)
        {
            _client = client;
        }
        public async Task<RatingInternalDTO> Create(RatingInternalDTO ratingDTO)
        {
            var content = JsonConvert.SerializeObject(ratingDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/ratinginternal", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var rating = JsonConvert.DeserializeObject<RatingInternalDTO>(result);
                return rating;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<RatingInternalDTO> Get(int ratingId)
        {
            var response = await _client.GetAsync($"api/ratinginternal/{ratingId}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var rating = JsonConvert.DeserializeObject<RatingInternalDTO>(result);
                return rating;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<RatingInternalDTO>> Getall(int counterpartyId)
        {
            var response = await _client.GetAsync($"api/ratinginternal/all/{counterpartyId}");
            var content = await response.Content.ReadAsStringAsync();
            var ratings = JsonConvert.DeserializeObject<IEnumerable<RatingInternalDTO>>(content);
            return ratings;
        }

        public async Task<RatingInternalDTO> Update(RatingInternalDTO ratingDTO)
        {
            var content = JsonConvert.SerializeObject(ratingDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"api/ratinginternal/{ratingDTO.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var department = JsonConvert.DeserializeObject<RatingInternalDTO>(result);
                return department;
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
            var response = await _client.DeleteAsync($"api/ratinginternal/{id}");
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
