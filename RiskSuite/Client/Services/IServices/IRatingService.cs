using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IRatingService
    {
        public Task<IEnumerable<RatingDTO>> Getall();
        public Task<RatingDTO> Get(int id);
        public Task<RatingDTO> Create(RatingDTO dto);
        Task<RatingDTO> Update(RatingDTO dto);
        Task<bool> Delete(int id);
    }
}
