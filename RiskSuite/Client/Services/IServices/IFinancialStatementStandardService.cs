using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IFinancialStatementStandardService
    {
        public Task<IEnumerable<FinancialStatementStandardDTO>> Getall();
        public Task<FinancialStatementStandardDTO> Get(int id);
        public Task<FinancialStatementStandardDTO> Create(FinancialStatementStandardDTO fssDTO);
        Task<FinancialStatementStandardDTO> Update(FinancialStatementStandardDTO fssDTO);
        Task<bool> Delete(int id);
    }
}
