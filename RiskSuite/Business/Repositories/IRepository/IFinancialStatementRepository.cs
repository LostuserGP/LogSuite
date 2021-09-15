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
    public interface IFinancialStatementRepository
    {
        public Task<FinancialStatementDTO> Create(FinancialStatementDTO fsDTO);
        public Task<FinancialStatementDTO> Update(FinancialStatementDTO fsDTO);
        public Task<FinancialStatementDTO> Get(int fsId);
        public Task<int> Delete(int fsId);
        public Task<IEnumerable<FinancialStatementDTO>> GetAll(int counterpartyId);
        public Task<FinancialStatementDTO> IsUnique(FinancialStatementDTO fsDTO, int fsId = 0);
    }
}
