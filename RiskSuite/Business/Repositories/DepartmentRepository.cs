using AutoMapper;
using Business.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using RiskSuite.Business;
using RiskSuite.Business.Repositories;
using RiskSuite.DataAccess;
using RiskSuite.Shared;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public DepartmentRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<DepartmentDTO> Create(DepartmentDTO departmentDTO)
        {
            Department department = _mapper.Map<DepartmentDTO, Department>(departmentDTO);
            var newDepartment = await _db.Departments.AddAsync(department);
            await _db.SaveChangesAsync();
            return _mapper.Map<Department, DepartmentDTO>(newDepartment.Entity);
        }

        public Task<int> Delete(int departmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<DepartmentDTO> Get(int departmentId)
        {
            var department = await _db.Departments.FindAsync(departmentId);
            var departmentDTO = _mapper.Map<Department, DepartmentDTO>(department);
            return departmentDTO;
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAll()
        {
            try
            {
                var departments = _db.Departments;
                IEnumerable<DepartmentDTO> departmentDTOs = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDTO>>(departments);
                return departmentDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PagedList<DepartmentDTO>> GetPaged(Params parameters)
        {
            var source = _db.Departments
                    .Include(x => x.ApplicationUsers)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<Department>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var departments = _mapper.Map<List<DepartmentDTO>>(result);

            return new PagedList<DepartmentDTO>(departments, result.MetaData);
        }

        public async Task<DepartmentDTO> IsUnique(DepartmentDTO departmentDTO, int departmentId = 0)
        {
            try
            {
                if (departmentId == 0)
                {
                    var departmentFromDb = await _db.Departments
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == departmentDTO.Name.ToLower()
                        || x.ShortName.ToLower() == departmentDTO.ShortName.ToLower()
                        || x.Code == departmentDTO.Code));
                    var result = _mapper.Map<Department, DepartmentDTO>(departmentFromDb);
                    return result;
                }
                else
                {
                    var departmentFromDb = await _db.Departments
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == departmentDTO.Name.ToLower()
                        || x.ShortName.ToLower() == departmentDTO.ShortName.ToLower()
                        || x.Code == departmentDTO.Code)
                        && x.Id != departmentId);
                    var result = _mapper.Map<Department, DepartmentDTO>(departmentFromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DepartmentDTO> Update(DepartmentDTO departmentDTO)
        {
            try
            {
                Department departmentFromDb = await _db.Departments.FindAsync(departmentDTO.Id);
                Department departmentToUpdate = _mapper.Map(departmentDTO, departmentFromDb);
                var updatedDepartment = _db.Departments.Update(departmentToUpdate);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<Department, DepartmentDTO>(updatedDepartment.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}