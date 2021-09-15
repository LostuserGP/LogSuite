using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IRatingInternalService
    {
        public Task<IEnumerable<RatingInternalDTO>> Getall(int counterpartyId);
        public Task<RatingInternalDTO> Get(int id);
        public Task<RatingInternalDTO> Create(RatingInternalDTO ratingDTO);
        Task<RatingInternalDTO> Update(RatingInternalDTO ratingDTO);
        Task<bool> Delete(int id);
    }
}
