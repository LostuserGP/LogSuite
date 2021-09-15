using AutoMapper;
using Business.Repositories.IRepository;
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

namespace Business.Repositories
{
    public class FinancialStatementRepository : IFinancialStatementRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public FinancialStatementRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<FinancialStatementDTO> Create(FinancialStatementDTO fsDTO)
        {
            FinancialStatement fs = _mapper.Map<FinancialStatementDTO, FinancialStatement>(fsDTO);
            var newFS = await _db.FinancialStatements.AddAsync(fs);
            await _db.SaveChangesAsync();
            return _mapper.Map<FinancialStatement, FinancialStatementDTO>(newFS.Entity);
        }

        public async Task<int> Delete(int fsId)
        {
            var fs = await _db.FinancialStatements
                .Where(x => x.Id == fsId)
                .FirstOrDefaultAsync();
            if (fs != null)
            {
                var ratings = await _db.RatingInternals.Where(x => x.FinancialStatementId == fsId).ToListAsync();
                if (ratings.Any())
                {
                    return -1;
                }
                _db.FinancialStatements.Remove(fs);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<FinancialStatementDTO> Get(int fsId)
        {
            var fs = await _db.FinancialStatements
                .Include(x => x.FinancialStatementStandard)
                .FirstOrDefaultAsync(x => x.Id == fsId);
            var fsDTO = _mapper.Map<FinancialStatement, FinancialStatementDTO>(fs);
            return fsDTO;
        }

        public async Task<IEnumerable<FinancialStatementDTO>> GetAll(int counterpartyId)
        {
            try
            {
                var fss = await _db.FinancialStatements
                    .Include(x => x.FinancialStatementStandard)
                    .Where(x => x.CounterpartyId == counterpartyId)
                    .OrderByDescending(x => x.DateStart)
                    .ToListAsync();
                IEnumerable<FinancialStatementDTO> fsDTOs = _mapper.Map<IEnumerable<FinancialStatement>, IEnumerable<FinancialStatementDTO>>(fss);
                return fsDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FinancialStatementDTO> IsUnique(FinancialStatementDTO fsDTO, int fsId = 0)
        {
            try
            {
                if (fsId == 0)
                {
                    var fsFromDb = await _db.FinancialStatements
                        .FirstOrDefaultAsync(x => x.CounterpartyId == fsDTO.CounterpartyId
                        && x.DateStart == fsDTO.DateStart && x.FinancialStatementStandardId == fsDTO.FinancialStatementStandardId);
                    var result = _mapper.Map<FinancialStatement, FinancialStatementDTO>(fsFromDb);
                    return result;
                }
                else
                {
                    var fsFromDb = await _db.FinancialStatements
                        .FirstOrDefaultAsync(x => x.CounterpartyId == fsDTO.CounterpartyId
                        && x.DateStart == fsDTO.DateStart && x.FinancialStatementStandardId == fsDTO.FinancialStatementStandardId
                        && x.Id != fsId);
                    var result = _mapper.Map<FinancialStatement, FinancialStatementDTO>(fsFromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FinancialStatementDTO> Update(FinancialStatementDTO fsDTO)
        {
            try
            {
                FinancialStatement fsFromDb = await _db.FinancialStatements.FindAsync(fsDTO.Id);
                fsFromDb.FinancialStatementStandardId = fsDTO.FinancialStatementStandardId;
                fsFromDb.DateStart = fsDTO.DateStart;
                fsFromDb.Comment = fsDTO.Comment;
                var updatedFS = _db.FinancialStatements.Update(fsFromDb);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<FinancialStatement, FinancialStatementDTO>(updatedFS.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
