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
    public interface ICommitteeRepository
    {
        public Task<CommitteeDTO> Create(CommitteeDTO fsDTO);
        public Task<CommitteeDTO> Update(CommitteeDTO fsDTO);
        public Task<CommitteeDTO> Get(int id);
        public Task<int> Delete(int id);
        public Task<IEnumerable<CommitteeDTO>> GetAll(int counterpartyId);
        public Task<CommitteeDTO> IsUnique(CommitteeDTO dto, int id = 0);
    }
}
