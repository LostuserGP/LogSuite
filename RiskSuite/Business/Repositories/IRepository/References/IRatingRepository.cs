using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository.References
{
    public interface IRatingRepository
    {
        public Task<RatingDTO> Create(RatingDTO dto);
        public Task<RatingDTO> Update(RatingDTO dto);
        public Task<RatingDTO> Get(int id);
        public Task<int> Delete(int id);
        public Task<IEnumerable<RatingDTO>> GetAll();
        public Task<RatingDTO> IsUnique(RatingDTO dto, int id = 0);
    }
}
