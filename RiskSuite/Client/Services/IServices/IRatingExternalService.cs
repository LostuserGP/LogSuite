using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IRatingExternalService
    {
        public Task<IEnumerable<RatingExternalDTO>> Getall(int counterpartyId);
        public Task<RatingExternalDTO> Get(int id);
        public Task<RatingExternalDTO> Create(RatingExternalDTO ratingDTO);
        Task<RatingExternalDTO> Update(RatingExternalDTO ratingDTO);
        Task<bool> Delete(int id);
    }
}
