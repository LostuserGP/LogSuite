using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogSuite.Shared.Models.CredRisk;

namespace LogSuite.Client.Services.IServices
{
    public interface IRatingAgencyService
    {
        public Task<IEnumerable<RatingAgencyDTO>> Getall();
        public Task<RatingAgencyDTO> Get(int id);
        public Task<RatingAgencyDTO> Create(RatingAgencyDTO dto);
        Task<RatingAgencyDTO> Update(RatingAgencyDTO dto);
        Task<bool> Delete(int id);
    }
}
