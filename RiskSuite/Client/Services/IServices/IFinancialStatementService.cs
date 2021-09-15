using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IFinancialStatementService
    {
        public Task<IEnumerable<FinancialStatementDTO>> Getall(int counterpartyId);
        public Task<FinancialStatementDTO> Get(int id);
        public Task<FinancialStatementDTO> Create(FinancialStatementDTO fsDTO);
        Task<FinancialStatementDTO> Update(FinancialStatementDTO fsDTO);
        Task<bool> Delete(int id);
    }
}
