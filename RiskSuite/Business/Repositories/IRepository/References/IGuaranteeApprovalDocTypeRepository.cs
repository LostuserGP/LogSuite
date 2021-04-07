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
    public interface IGuaranteeApprovalDocTypeRepository
    {
        public Task<GuaranteeApprovalDocTypeDTO> Create(GuaranteeApprovalDocTypeDTO dto);
        public Task<GuaranteeApprovalDocTypeDTO> Update(GuaranteeApprovalDocTypeDTO dto);
        public Task<GuaranteeApprovalDocTypeDTO> Get(int id);
        public Task<int> Delete(int id);
        public Task<IEnumerable<GuaranteeApprovalDocTypeDTO>> GetAll();
        public Task<GuaranteeApprovalDocTypeDTO> IsUnique(GuaranteeApprovalDocTypeDTO dto, int id = 0);
    }
}
