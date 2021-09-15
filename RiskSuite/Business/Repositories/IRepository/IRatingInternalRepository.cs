using LogSuite.Business;
using LogSuite.DataAccess.CredRisk;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IRatingInternalRepository
    {
        public Task<RatingInternalDTO> Create(RatingInternalDTO ratingDTO);
        public Task<RatingInternalDTO> Update(RatingInternalDTO ratingDTO);
        public Task<RatingInternalDTO> Get(int ratingId);
        public Task<int> Delete(int ratingId);
        public Task<IEnumerable<RatingInternalDTO>> GetAll(int counterpartyId);
        public Task<RatingInternalDTO> IsUnique(RatingInternalDTO ratingDTO, int ratingId = 0);
    }
}
