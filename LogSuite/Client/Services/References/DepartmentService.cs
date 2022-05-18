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
using LogSuite.Shared.Helpers;

namespace LogSuite.Client.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _client;

        public DepartmentService(HttpClient client)
        {
            _client = client;
        }
        public async Task<DepartmentDTO> Create(DepartmentDTO departmentDTO)
        {
            var content = JsonConvert.SerializeObject(departmentDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/department", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var department = JsonConvert.DeserializeObject<DepartmentDTO>(result);
                return department;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<DepartmentDTO> Get(int departmentId)
        {
            var response = await _client.GetAsync($"api/department/{departmentId}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var department = JsonConvert.DeserializeObject<DepartmentDTO>(result);
                return department;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<DepartmentDTO>> Getall()
        {
            var response = await _client.GetAsync($"api/department");
            var content = await response.Content.ReadAsStringAsync();
            var departments = JsonConvert.DeserializeObject<IEnumerable<DepartmentDTO>>(content);
            return departments;
        }

        public async Task<PagingResponse<DepartmentDTO>> Getall(Params parameters)
        {
            var queryStringParam = StringParser.ParamsToDict(parameters);
            var response = await _client.GetAsync(QueryHelpers.AddQueryString("api/department", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<DepartmentDTO>
            {
                Items = System.Text.Json.JsonSerializer.Deserialize<List<DepartmentDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = System.Text.Json.JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task<DepartmentDTO> Update(DepartmentDTO departmentDTO)
        {
            var content = JsonConvert.SerializeObject(departmentDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"api/department/{departmentDTO.Id}", bodyContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var department = JsonConvert.DeserializeObject<DepartmentDTO>(result);
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
            var response = await _client.DeleteAsync($"api/department/{id}");
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
