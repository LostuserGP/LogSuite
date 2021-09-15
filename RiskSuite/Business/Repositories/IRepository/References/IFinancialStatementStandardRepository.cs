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
    public interface IFinancialStatementStandardRepository
    {
        public Task<FinancialStatementStandardDTO> Create(FinancialStatementStandardDTO dto);
        public Task<FinancialStatementStandardDTO> Update(FinancialStatementStandardDTO dto);
        public Task<FinancialStatementStandardDTO> Get(int id);
        public Task<int> Delete(int id);
        public Task<IEnumerable<FinancialStatementStandardDTO>> GetAll();
        public Task<FinancialStatementStandardDTO> IsUnique(FinancialStatementStandardDTO dto, int id = 0);
    }
}
