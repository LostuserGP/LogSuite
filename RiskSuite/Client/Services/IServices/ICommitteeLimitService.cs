using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface ICommitteeLimitService
    {
        public Task<IEnumerable<CommitteeLimitDTO>> Getall();
        public Task<CommitteeLimitDTO> Get(int id);
        public Task<CommitteeLimitDTO> Create(CommitteeLimitDTO dto);
        public Task<PagingResponse<CommitteeLimitDTO>> Getall(Params parameters);
        Task<CommitteeLimitDTO> Update(CommitteeLimitDTO dto);
        Task<bool> Delete(int id);
    }
}
