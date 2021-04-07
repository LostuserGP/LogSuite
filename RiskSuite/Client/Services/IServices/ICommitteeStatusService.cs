using RiskSuite.Client.Helpers;
using RiskSuite.Shared;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskSuite.Client.Services.IServices
{
    public interface ICommitteeStatusService
    {
        public Task<IEnumerable<CommitteeStatusDTO>> Getall();
        public Task<CommitteeStatusDTO> Get(int id);
        public Task<CommitteeStatusDTO> Create(CommitteeStatusDTO dto);
        public Task<PagingResponse<CommitteeStatusDTO>> Getall(Params parameters);
        Task<CommitteeStatusDTO> Update(CommitteeStatusDTO dto);
        Task<bool> Delete(int id);
    }
}
