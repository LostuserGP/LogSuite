using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.Business.Repositories.IRepository.References;
using LogSuite.DataAccess;
using LogSuite.DataAccess.CredRisk;
using LogSuite.Shared;
using LogSuite.Shared.Models.CredRisk;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.References
{
    public class RiskClassRepository : IRiskClassRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RiskClassRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<RiskClassDTO> Create(RiskClassDTO dto)
        {
            var toDb = _mapper.Map<RiskClassDTO, RiskClass>(dto);
            var result = await _db.RiskClasses.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<RiskClass, RiskClassDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.RiskClasses.FindAsync(id);
            if (fromDb == null) return 0;
            var parent = await _db.RatingInternals
                .Where(x => x.RiskClassId == id)
                .AnyAsync();
            if (parent)
            {
                return -1;
            }

            _db.RiskClasses.Remove(fromDb);
            return await _db.SaveChangesAsync();
        }

        public async Task<RiskClassDTO> Get(int id)
        {
            var fromDb = await _db.RiskClasses.FindAsync(id);
            var dto = _mapper.Map<RiskClass, RiskClassDTO>(fromDb);
            return dto;
        }

        public Task<IEnumerable<RiskClassDTO>> GetAll()
        {
            try
            {
                var entities = _db.RiskClasses;
                var dtos =
                    _mapper.Map<IEnumerable<RiskClass>, IEnumerable<RiskClassDTO>>(entities);
                return Task.FromResult(dtos);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IEnumerable<RiskClassDTO>>(null);
            }
        }

        public async Task<RiskClassDTO> IsUnique(RiskClassDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.RiskClasses
                        .FirstOrDefaultAsync(x => x.Name.ToLower() == dto.Name.ToLower());
                    var result = _mapper.Map<RiskClass, RiskClassDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.RiskClasses
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == dto.Name.ToLower())
                                                  && x.Id != id);
                    var result = _mapper.Map<RiskClass, RiskClassDTO>(fromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PagedList<RiskClassDTO>> GetPaged(Params parameters)
        {
            var source = _db.RiskClasses
                .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result = await PagedList<RiskClass>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<RiskClassDTO>>(result);

            return new PagedList<RiskClassDTO>(entities, result.MetaData);
        }

        public async Task<RiskClassDTO> Update(RiskClassDTO dto)
        {
            var fromDb = await _db.RiskClasses.FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.RiskClasses.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<RiskClass, RiskClassDTO>(updated.Entity);
            return result;
        }
    }
}