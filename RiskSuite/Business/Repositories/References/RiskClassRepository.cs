using AutoMapper;
using Business.Repositories.IRepository.References;
using Microsoft.EntityFrameworkCore;
using LogSuite.Business;
using LogSuite.Business.Repositories;
using LogSuite.DataAccess;
using LogSuite.DataAccess.CredRisk;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.References
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
            RiskClass toDb = _mapper.Map<RiskClassDTO, RiskClass>(dto);
            var result = await _db.RiskClasses.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<RiskClass, RiskClassDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.RiskClasses.FindAsync(id);
            if (fromDb != null)
            {
                var parent = await _db.RatingInternals
                    .Where(x => x.RiskClassId == id)
                    .AnyAsync();
                if (parent)
                {
                    return -1;
                }
                else
                {
                    _db.RiskClasses.Remove(fromDb);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<RiskClassDTO> Get(int id)
        {
            var fromDb = await _db.RiskClasses.FindAsync(id);
            var dto = _mapper.Map<RiskClass, RiskClassDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<RiskClassDTO>> GetAll()
        {
            try
            {
                var entities = _db.RiskClasses;
                IEnumerable<RiskClassDTO> dtos = _mapper.Map<IEnumerable<RiskClass>, IEnumerable<RiskClassDTO>>(entities);
                return dtos;
            }
            catch (Exception ex)
            {
                return null;
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

        public async Task<RiskClassDTO> Update(RiskClassDTO dto)
        {
            try
            {
                var fromDb = await _db.RiskClasses.FindAsync(dto.Id);
                var toUpdate = _mapper.Map(dto, fromDb);
                var updated = _db.RiskClasses.Update(toUpdate);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<RiskClass, RiskClassDTO>(updated.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}