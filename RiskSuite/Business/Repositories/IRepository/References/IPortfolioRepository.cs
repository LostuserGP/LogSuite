using RiskSuite.Business;
using RiskSuite.Shared;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository.References
{
    public interface IPortfolioRepository
    {
        public Task<PortfolioDTO> Create(PortfolioDTO dto);
        public Task<PortfolioDTO> Update(PortfolioDTO dto);
        public Task<PortfolioDTO> Get(int id);
        public Task<int> Delete(int id);
        public Task<IEnumerable<PortfolioDTO>> GetAll();
        public Task<PortfolioDTO> IsUnique(PortfolioDTO dto, int id = 0);
    }
}
