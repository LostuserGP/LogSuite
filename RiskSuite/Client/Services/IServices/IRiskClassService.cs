using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IRiskClassService
    {
        public Task<IEnumerable<RiskClassDTO>> Getall();
        public Task<RiskClassDTO> Get(int id);
        public Task<RiskClassDTO> Create(RiskClassDTO dto);
        Task<RiskClassDTO> Update(RiskClassDTO dto);
        Task<bool> Delete(int id);
    }
}
