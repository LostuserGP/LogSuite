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
    public interface ICommitteeStatusRepository
    {
        public Task<CommitteeStatusDTO> Create(CommitteeStatusDTO dto);
        public Task<CommitteeStatusDTO> Update(CommitteeStatusDTO dto);
        public Task<CommitteeStatusDTO> Get(int id);
        public Task<int> Delete(int id);
        public Task<IEnumerable<CommitteeStatusDTO>> GetAll();
        public Task<CommitteeStatusDTO> IsUnique(CommitteeStatusDTO dto, int id = 0);
    }
}
