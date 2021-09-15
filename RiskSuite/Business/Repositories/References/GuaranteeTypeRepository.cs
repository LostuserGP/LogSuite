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
    public class GuaranteeTypeRepository : IGuaranteeTypeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GuaranteeTypeRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<GuaranteeTypeDTO> Create(GuaranteeTypeDTO dto)
        {
            GuaranteeType toDb = _mapper.Map<GuaranteeTypeDTO, GuaranteeType>(dto);
            var result = await _db.GuaranteeTypes.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GuaranteeType, GuaranteeTypeDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GuaranteeTypes.FindAsync(id);
            if (fromDb != null)
            {
                var parent = await _db.Guarantees
                    .Where(x => x.GuaranteeTypeId == id)
                    .AnyAsync();
                if (parent)
                {
                    return -1;
                }
                else
                {
                    _db.GuaranteeTypes.Remove(fromDb);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<GuaranteeTypeDTO> Get(int id)
        {
            var fromDb = await _db.GuaranteeTypes.FindAsync(id);
            var dto = _mapper.Map<GuaranteeType, GuaranteeTypeDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<GuaranteeTypeDTO>> GetAll()
        {
            try
            {
                var entities = _db.GuaranteeTypes;
                IEnumerable<GuaranteeTypeDTO> dtos = _mapper.Map<IEnumerable<GuaranteeType>, IEnumerable<GuaranteeTypeDTO>>(entities);
                return dtos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<GuaranteeTypeDTO> IsUnique(GuaranteeTypeDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.GuaranteeTypes
                        .FirstOrDefaultAsync(x => x.Name.ToLower() == dto.Name.ToLower());
                    var result = _mapper.Map<GuaranteeType, GuaranteeTypeDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.GuaranteeTypes
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == dto.Name.ToLower())
                        && x.Id != id);
                    var result = _mapper.Map<GuaranteeType, GuaranteeTypeDTO>(fromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<GuaranteeTypeDTO> Update(GuaranteeTypeDTO dto)
        {
            try
            {
                var fromDb = await _db.GuaranteeTypes.FindAsync(dto.Id);
                var toUpdate = _mapper.Map(dto, fromDb);
                var updated = _db.GuaranteeTypes.Update(toUpdate);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<GuaranteeType, GuaranteeTypeDTO>(updated.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}