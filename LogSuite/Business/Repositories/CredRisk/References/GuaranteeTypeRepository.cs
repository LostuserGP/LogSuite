using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.Business.Repositories.IRepository.ICredRisk.IReferences;
using LogSuite.Business.Repositories.IRepository.References;
using LogSuite.Business.Repositories.References;
using LogSuite.DataAccess;
using LogSuite.DataAccess.CredRisk;
using LogSuite.Shared;
using LogSuite.Shared.Models.CredRisk;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.CredRisk.References
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
            if (fromDb == null) return 0;
            var parent = await _db.Guarantees
                .Where(x => x.GuaranteeTypeId == id)
                .AnyAsync();
            if (parent)
            {
                return -1;
            }

            _db.GuaranteeTypes.Remove(fromDb);
            return await _db.SaveChangesAsync();
        }

        public async Task<GuaranteeTypeDTO> Get(int id)
        {
            var fromDb = await _db.GuaranteeTypes.FindAsync(id);
            var dto = _mapper.Map<GuaranteeType, GuaranteeTypeDTO>(fromDb);
            return dto;
        }

        public Task<IEnumerable<GuaranteeTypeDTO>> GetAll()
        {
            try
            {
                var entities = _db.GuaranteeTypes;
                var dtos = _mapper.Map<IEnumerable<GuaranteeType>, IEnumerable<GuaranteeTypeDTO>>(entities);
                return Task.FromResult(dtos);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IEnumerable<GuaranteeTypeDTO>>(null);
            }
        }

        public async Task<GuaranteeTypeDTO> IsUnique(GuaranteeTypeDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.GuaranteeTypes
                        .FirstOrDefaultAsync(x => string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase));
                    var result = _mapper.Map<GuaranteeType, GuaranteeTypeDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.GuaranteeTypes
                        .FirstOrDefaultAsync(x => (string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase))
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

        public async Task<PagedList<GuaranteeTypeDTO>> GetPaged(Params parameters)
        {
            var source = _db.GuaranteeTypes
                .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result = await PagedList<GuaranteeType>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<GuaranteeTypeDTO>>(result);

            return new PagedList<GuaranteeTypeDTO>(entities, result.MetaData);
        }

        public async Task<GuaranteeTypeDTO> Update(GuaranteeTypeDTO dto)
        {
            var fromDb = await _db.GuaranteeTypes.FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.GuaranteeTypes.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GuaranteeType, GuaranteeTypeDTO>(updated.Entity);
            return result;
        }
    }
}