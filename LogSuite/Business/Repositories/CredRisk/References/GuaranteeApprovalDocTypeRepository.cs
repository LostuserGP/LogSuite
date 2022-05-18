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
    public class GuaranteeApprovalDocTypeRepository : IGuaranteeApprovalDocTypeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GuaranteeApprovalDocTypeRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GuaranteeApprovalDocTypeDTO> Create(GuaranteeApprovalDocTypeDTO dto)
        {
            var toDb = _mapper.Map<GuaranteeApprovalDocTypeDTO, GuaranteeApprovalDocType>(dto);
            var result = await _db.GuaranteeApprovalDocTypes.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GuaranteeApprovalDocType, GuaranteeApprovalDocTypeDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GuaranteeApprovalDocTypes.FindAsync(id);
            if (fromDb == null) return 0;
            var parent = await _db.GuaranteeApprovalDocs
                .Where(x => x.GuaranteeApprovalDocTypeId == id)
                .AnyAsync();
            if (parent)
            {
                return -1;
            }

            _db.GuaranteeApprovalDocTypes.Remove(fromDb);
            return await _db.SaveChangesAsync();
        }

        public async Task<GuaranteeApprovalDocTypeDTO> Get(int id)
        {
            var fromDb = await _db.GuaranteeApprovalDocTypes.FindAsync(id);
            var dto = _mapper.Map<GuaranteeApprovalDocType, GuaranteeApprovalDocTypeDTO>(fromDb);
            return dto;
        }

        public Task<IEnumerable<GuaranteeApprovalDocTypeDTO>> GetAll()
        {
            try
            {
                var entities = _db.GuaranteeApprovalDocTypes;
                var dtos =
                    _mapper.Map<IEnumerable<GuaranteeApprovalDocType>, IEnumerable<GuaranteeApprovalDocTypeDTO>>(
                        entities);
                return Task.FromResult(dtos);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IEnumerable<GuaranteeApprovalDocTypeDTO>>(null);
            }
        }

        public async Task<GuaranteeApprovalDocTypeDTO> IsUnique(GuaranteeApprovalDocTypeDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.GuaranteeApprovalDocTypes
                        .FirstOrDefaultAsync(x =>
                            string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase));
                    var result = _mapper.Map<GuaranteeApprovalDocType, GuaranteeApprovalDocTypeDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.GuaranteeApprovalDocTypes
                        .FirstOrDefaultAsync(x =>
                            (string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase))
                            && x.Id != id);
                    var result = _mapper.Map<GuaranteeApprovalDocType, GuaranteeApprovalDocTypeDTO>(fromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PagedList<GuaranteeApprovalDocTypeDTO>> GetPaged(Params parameters)
        {
            var source = _db.GuaranteeApprovalDocTypes
                .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result = await PagedList<GuaranteeApprovalDocType>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<GuaranteeApprovalDocTypeDTO>>(result);

            return new PagedList<GuaranteeApprovalDocTypeDTO>(entities, result.MetaData);
        }

        public async Task<GuaranteeApprovalDocTypeDTO> Update(GuaranteeApprovalDocTypeDTO dto)
        {
            var fromDb = await _db.GuaranteeApprovalDocTypes.FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.GuaranteeApprovalDocTypes.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GuaranteeApprovalDocType, GuaranteeApprovalDocTypeDTO>(updated.Entity);
            return result;
        }
    }
}