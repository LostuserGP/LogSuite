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
    public interface IGuaranteeTypeRepository
    {
        public Task<GuaranteeTypeDTO> Create(GuaranteeTypeDTO dto);
        public Task<GuaranteeTypeDTO> Update(GuaranteeTypeDTO dto);
        public Task<GuaranteeTypeDTO> Get(int id);
        public Task<int> Delete(int id);
        public Task<IEnumerable<GuaranteeTypeDTO>> GetAll();
        public Task<GuaranteeTypeDTO> IsUnique(GuaranteeTypeDTO dto, int id = 0);
    }
}
