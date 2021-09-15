using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Client.Services
{
    public class CommitteeService : ICommitteeService
    {
        private readonly HttpClient _client;
        private readonly string _apiUrl = "api/committee";

        public CommitteeService(HttpClient client)
        {
            _client = client;
        }
        public async Task<CommitteeDTO> Create(CommitteeDTO dto)
        {
            var content = JsonConvert.SerializeObject(dto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_apiUrl, bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var answer = JsonConvert.DeserializeObject<CommitteeDTO>(result);
                return answer;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<CommitteeDTO> Get(int id)
        {
            var response = await _client.GetAsync($"{_apiUrl}/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var rating = JsonConvert.DeserializeObject<CommitteeDTO>(result);
                return rating;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<CommitteeDTO>> Getall(int counterpartyId)
        {
            var response = await _client.GetAsync($"{_apiUrl}/all/{counterpartyId}");
            var content = await response.Content.ReadAsStringAsync();
            var ratings = JsonConvert.DeserializeObject<IEnumerable<CommitteeDTO>>(content);
            return ratings;
        }

        public async Task<CommitteeDTO> Update(CommitteeDTO ratingDTO)
        {
            var content = JsonConvert.SerializeObject(ratingDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_apiUrl}/{ratingDTO.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<CommitteeDTO>(result);
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
            var response = await _client.DeleteAsync($"{_apiUrl}/{id}");
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
