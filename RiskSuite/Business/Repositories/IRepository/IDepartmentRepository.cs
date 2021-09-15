using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IDepartmentRepository
    {
        public Task<DepartmentDTO> Create(DepartmentDTO departmentDTO);
        public Task<DepartmentDTO> Update(DepartmentDTO departmentDTO);
        public Task<DepartmentDTO> Get(int departmentId);
        public Task<int> Delete(int departmentId);
        public Task<IEnumerable<DepartmentDTO>> GetAll();
        public Task<DepartmentDTO> IsUnique(DepartmentDTO departmentDTO, int departmentId = 0);
        public Task<PagedList<DepartmentDTO>> GetPaged(Params parameters);
    }
}
