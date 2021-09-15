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
    public interface IRatingExternalRepository
    {
        public Task<RatingExternalDTO> Create(RatingExternalDTO ratingDTO);
        public Task<RatingExternalDTO> Update(RatingExternalDTO ratingDTO);
        public Task<RatingExternalDTO> Get(int ratingId);
        public Task<int> Delete(int ratingId);
        public Task<IEnumerable<RatingExternalDTO>> GetAll(int counterpartyId);
        public Task<RatingExternalDTO> IsUnique(RatingExternalDTO ratingDTO, int ratingId = 0);
    }
}
