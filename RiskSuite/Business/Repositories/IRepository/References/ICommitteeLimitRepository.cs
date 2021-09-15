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
    public interface ICommitteeLimitRepository
    {
        public Task<CommitteeLimitDTO> Create(CommitteeLimitDTO dto);
        public Task<CommitteeLimitDTO> Update(CommitteeLimitDTO dto);
        public Task<CommitteeLimitDTO> Get(int id);
        public Task<int> Delete(int id);
        public Task<IEnumerable<CommitteeLimitDTO>> GetAll();
        public Task<CommitteeLimitDTO> IsUnique(CommitteeLimitDTO dto, int id = 0);
    }
}
