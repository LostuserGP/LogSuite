using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.DataAccess;
using LogSuite.DataAccess.References;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.References
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

        public async Task<DepartmentDTO> Create(DepartmentDTO dto)
        {
            var department = _mapper.Map<DepartmentDTO, Department>(dto);
            var newDepartment = await _db.Departments.AddAsync(department);
            await _db.SaveChangesAsync();
            return _mapper.Map<Department, DepartmentDTO>(newDepartment.Entity);
        }

        public async Task<int> Delete(int departmentId)
        {
            var department = await _db.Departments
                .Include(x => x.ApplicationUsers)
                .Where(x => x.Id == departmentId)
                .FirstOrDefaultAsync();
            if (department == null) return 0;
            if (department.ApplicationUsers.Any())
            {
                return -1;
            }

            _db.Departments.Remove(department);
            return await _db.SaveChangesAsync();
        }

        public async Task<DepartmentDTO> Get(int id)
        {
            var department = await _db.Departments.FindAsync(id);
            var departmentDto = _mapper.Map<Department, DepartmentDTO>(department);
            return departmentDto;
        }

        public Task<IEnumerable<DepartmentDTO>> GetAll()
        {
            try
            {
                var departments = _db.Departments;
                var departmentDtOs =
                    _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDTO>>(departments);
                return Task.FromResult(departmentDtOs);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IEnumerable<DepartmentDTO>>(null);
            }
        }

        public async Task<PagedList<DepartmentDTO>> GetPaged(Params parameters)
        {
            var source = _db.Departments
                .Include(x => x.ApplicationUsers)
                .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result =
                await PagedList<Department>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var departments = _mapper.Map<List<DepartmentDTO>>(result);

            return new PagedList<DepartmentDTO>(departments, result.MetaData);
        }

        public async Task<DepartmentDTO> IsUnique(DepartmentDTO dto, int departmentId = 0)
        {
            try
            {
                if (departmentId == 0)
                {
                    var departmentFromDb = await _db.Departments
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == dto.Name.ToLower()
                                                   || x.ShortName.ToLower() == dto.ShortName.ToLower()
                                                   || x.Code == dto.Code));
                    var result = _mapper.Map<Department, DepartmentDTO>(departmentFromDb);
                    return result;
                }
                else
                {
                    var departmentFromDb = await _db.Departments
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == dto.Name.ToLower()
                                                   || x.ShortName.ToLower() == dto.ShortName.ToLower()
                                                   || x.Code == dto.Code)
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

        public async Task<DepartmentDTO> Update(DepartmentDTO dto)
        {
            var departmentFromDb = await _db.Departments.FindAsync(dto.Id);
            var departmentToUpdate = _mapper.Map(dto, departmentFromDb);
            if (departmentToUpdate == null) return null;
            var updatedDepartment = _db.Departments.Update(departmentToUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<Department, DepartmentDTO>(updatedDepartment.Entity);
            return result;
        }
    }
}