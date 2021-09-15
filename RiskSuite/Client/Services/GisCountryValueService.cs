using LogSuite.Client.Helpers;
using LogSuite.Client.Serices;
using LogSuite.Client.Services.IServices;
using LogSuite.Shared;
using LogSuite.Shared.Models.Operativka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LogSuite.Client.Services
{
    public class GisCountryValueService : IGisCountryValueService
    {
        private readonly HttpClient _client;
        private readonly ToastService _toastService;
        private readonly string _apirUrl = "api/giscountry";

        public GisCountryValueService(HttpClient client, ToastService toastService)
        {
            _client = client;
            _toastService = toastService;
        }

        public async Task<GisCountryDTO> Create(GisCountryValueDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GisCountryDTO> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GisCountryValueDTO>> GetAllByGisCountryId(int gisCountryId)
        {
            throw new NotImplementedException();
        }

        public async Task<GisCountryValueDTO> GetOnDateByGisCountryId(int gisCountryId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GisCountryValueDTO>> GetOnDateRangeByGisCountryId(int gisCountryId, DateTime dateStart, DateTime dateEnd)
        {
            throw new NotImplementedException();
        }

        public async Task<PagingResponse<GisCountryValueDTO>> GetPagedByGisCountryId(int gisCountryId, Params parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<GisCountryDTO> Update(GisCountryValueDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
