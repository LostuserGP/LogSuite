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
    public interface IRiskClassRepository
    {
        public Task<RiskClassDTO> Create(RiskClassDTO dto);
        public Task<RiskClassDTO> Update(RiskClassDTO dto);
        public Task<RiskClassDTO> Get(int id);
        public Task<int> Delete(int id);
        public Task<IEnumerable<RiskClassDTO>> GetAll();
        public Task<RiskClassDTO> IsUnique(RiskClassDTO dto, int id = 0);
    }
}
