using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskSuite.Client.Services.IServices
{
    public interface IDepartmentService
    {
        public Task<IEnumerable<DepartmentDTO>> Getall();
        public Task<DepartmentDTO> Get(int departmentId);
        public Task<DepartmentDTO> Create(DepartmentDTO departmentDTO);
    }
}
