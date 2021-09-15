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
            GuaranteeApprovalDocType toDb = _mapper.Map<GuaranteeApprovalDocTypeDTO, GuaranteeApprovalDocType>(dto);
            var result = await _db.GuaranteeApprovalDocTypes.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GuaranteeApprovalDocType, GuaranteeApprovalDocTypeDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GuaranteeApprovalDocTypes.FindAsync(id);
            if (fromDb != null)
            {
                var parent = await _db.GuaranteeApprovalDocs
                    .Where(x => x.GuaranteeApprovalDocTypeId == id)
                    .AnyAsync();
                if (parent)
                {
                    return -1;
                }
                else
                {
                    _db.GuaranteeApprovalDocTypes.Remove(fromDb);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<GuaranteeApprovalDocTypeDTO> Get(int id)
        {
            var fromDb = await _db.GuaranteeApprovalDocTypes.FindAsync(id);
            var dto = _mapper.Map<GuaranteeApprovalDocType, GuaranteeApprovalDocTypeDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<GuaranteeApprovalDocTypeDTO>> GetAll()
        {
            try
            {
                var entities = _db.GuaranteeApprovalDocTypes;
                IEnumerable<GuaranteeApprovalDocTypeDTO> dtos = _mapper.Map<IEnumerable<GuaranteeApprovalDocType>, IEnumerable<GuaranteeApprovalDocTypeDTO>>(entities);
                return dtos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<GuaranteeApprovalDocTypeDTO> IsUnique(GuaranteeApprovalDocTypeDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.GuaranteeApprovalDocTypes
                        .FirstOrDefaultAsync(x => x.Name.ToLower() == dto.Name.ToLower());
                    var result = _mapper.Map<GuaranteeApprovalDocType, GuaranteeApprovalDocTypeDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.GuaranteeApprovalDocTypes
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == dto.Name.ToLower())
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

        public async Task<GuaranteeApprovalDocTypeDTO> Update(GuaranteeApprovalDocTypeDTO dto)
        {
            try
            {
                var fromDb = await _db.GuaranteeApprovalDocTypes.FindAsync(dto.Id);
                var toUpdate = _mapper.Map(dto, fromDb);
                var updated = _db.GuaranteeApprovalDocTypes.Update(toUpdate);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<GuaranteeApprovalDocType, GuaranteeApprovalDocTypeDTO>(updated.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}