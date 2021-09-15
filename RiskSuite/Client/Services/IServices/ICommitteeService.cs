using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface ICommitteeService
    {
        public Task<IEnumerable<CommitteeDTO>> Getall(int counterpartyId);
        public Task<CommitteeDTO> Get(int id);
        public Task<CommitteeDTO> Create(CommitteeDTO dto);
        Task<CommitteeDTO> Update(CommitteeDTO dto);
        Task<bool> Delete(int id);
    }
}
