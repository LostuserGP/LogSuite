using LogSuite.Client.Helpers;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Client.Services.IServices
{
    public interface IDepartmentService
    {
        public Task<IEnumerable<DepartmentDTO>> Getall();
        public Task<DepartmentDTO> Get(int id);
        public Task<DepartmentDTO> Create(DepartmentDTO departmentDTO);
        public Task<PagingResponse<DepartmentDTO>> Getall(Params parameters);
        Task<DepartmentDTO> Update(DepartmentDTO departmentDTO);
        Task<bool> Delete(int id);
    }
}
