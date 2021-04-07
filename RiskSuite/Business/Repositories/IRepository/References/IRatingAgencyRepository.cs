using RiskSuite.Business;
using RiskSuite.Shared;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository.References
{
    public interface IRatingAgencyRepository
    {
        public Task<RatingAgencyDTO> Create(RatingAgencyDTO dto);
        public Task<RatingAgencyDTO> Update(RatingAgencyDTO dto);
        public Task<RatingAgencyDTO> Get(int id);
        public Task<int> Delete(int id);
        public Task<IEnumerable<RatingAgencyDTO>> GetAll();
        public Task<RatingAgencyDTO> IsUnique(RatingAgencyDTO dto, int id = 0);
    }
}
