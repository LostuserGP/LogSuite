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
    public class FinancialStatementStandardRepository : IFinancialStatementStandardRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public FinancialStatementStandardRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<FinancialStatementStandardDTO> Create(FinancialStatementStandardDTO dto)
        {
            FinancialStatementStandard toDb = _mapper.Map<FinancialStatementStandardDTO, FinancialStatementStandard>(dto);
            var result = await _db.FinancialStatementStandards.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<FinancialStatementStandard, FinancialStatementStandardDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.FinancialStatementStandards.FindAsync(id);
            if (fromDb != null)
            {
                var parent = await _db.FinancialStatements
                    .Where(x => x.FinancialStatementStandardId == id)
                    .AnyAsync();
                if (parent)
                {
                    return -1;
                }
                else
                {
                    _db.FinancialStatementStandards.Remove(fromDb);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<FinancialStatementStandardDTO> Get(int id)
        {
            var fromDb = await _db.FinancialStatementStandards.FindAsync(id);
            var dto = _mapper.Map<FinancialStatementStandard, FinancialStatementStandardDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<FinancialStatementStandardDTO>> GetAll()
        {
            try
            {
                var entities = _db.FinancialStatementStandards;
                IEnumerable<FinancialStatementStandardDTO> dtos = _mapper.Map<IEnumerable<FinancialStatementStandard>, IEnumerable<FinancialStatementStandardDTO>>(entities);
                return dtos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FinancialStatementStandardDTO> IsUnique(FinancialStatementStandardDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.FinancialStatementStandards
                        .FirstOrDefaultAsync(x => x.Name.ToLower() == dto.Name.ToLower());
                    var result = _mapper.Map<FinancialStatementStandard, FinancialStatementStandardDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.FinancialStatementStandards
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == dto.Name.ToLower())
                        && x.Id != id);
                    var result = _mapper.Map<FinancialStatementStandard, FinancialStatementStandardDTO>(fromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FinancialStatementStandardDTO> Update(FinancialStatementStandardDTO dto)
        {
            try
            {
                var fromDb = await _db.FinancialStatementStandards.FindAsync(dto.Id);
                var toUpdate = _mapper.Map(dto, fromDb);
                var updated = _db.FinancialStatementStandards.Update(toUpdate);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<FinancialStatementStandard, FinancialStatementStandardDTO>(updated.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}