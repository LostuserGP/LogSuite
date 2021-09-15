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
    public class RatingInternalRepository : IRatingInternalRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RatingInternalRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<RatingInternalDTO> Create(RatingInternalDTO ratingDTO)
        {
            RatingInternal rating = _mapper.Map<RatingInternalDTO, RatingInternal>(ratingDTO);
            var newRating = await _db.RatingInternals.AddAsync(rating);
            await _db.SaveChangesAsync();
            return _mapper.Map<RatingInternal, RatingInternalDTO>(newRating.Entity);
        }

        public async Task<int> Delete(int ratingId)
        {
            var rating = await _db.RatingInternals
                .Where(x => x.Id == ratingId)
                .FirstOrDefaultAsync();
            if (rating != null)
            {
                _db.RatingInternals.Remove(rating);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<RatingInternalDTO> Get(int ratingId)
        {
            var rating = await _db.RatingInternals.FindAsync(ratingId);
            var ratingDTO = _mapper.Map<RatingInternal, RatingInternalDTO>(rating);
            return ratingDTO;
        }

        public async Task<IEnumerable<RatingInternalDTO>> GetAll(int counterpartyId)
        {
            try
            {
                var ratings = await _db.RatingInternals
                    .Include(x => x.Rating)
                    .Include(x => x.RatingWc)
                    .Include(x => x.RiskClass)
                    .Include(x => x.FinancialStatement).ThenInclude(x => x.FinancialStatementStandard)
                    .Where(x => x.CounterpartyId == counterpartyId)
                    .OrderByDescending(x => x.DateStart)
                    .ToListAsync();
                IEnumerable<RatingInternalDTO> ratingDTOs = _mapper.Map<IEnumerable<RatingInternal>, IEnumerable<RatingInternalDTO>>(ratings);
                return ratingDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RatingInternalDTO> IsUnique(RatingInternalDTO ratingDTO, int ratingId = 0)
        {
            try
            {
                if (ratingId == 0)
                {
                    var ratingFromDb = await _db.RatingInternals
                        .FirstOrDefaultAsync(x => x.CounterpartyId == ratingDTO.CounterpartyId
                        && x.DateStart == ratingDTO.DateStart && x.RatingId == ratingDTO.RatingId);
                    var result = _mapper.Map<RatingInternal, RatingInternalDTO>(ratingFromDb);
                    return result;
                }
                else
                {
                    var ratingFromDb = await _db.RatingInternals
                        .FirstOrDefaultAsync(x => x.CounterpartyId == ratingDTO.CounterpartyId
                        && x.DateStart == ratingDTO.DateStart && x.RatingId == ratingDTO.RatingId
                        && x.Id != ratingId);
                    var result = _mapper.Map<RatingInternal, RatingInternalDTO>(ratingFromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RatingInternalDTO> Update(RatingInternalDTO ratingDTO)
        {
            try
            {
                RatingInternal ratingFromDb = await _db.RatingInternals.FindAsync(ratingDTO.Id);
                ratingFromDb.RatingId = ratingDTO.RatingId;
                ratingFromDb.RatingWcId = ratingDTO.RatingWcId;
                ratingFromDb.RiskClassId = ratingDTO.RiskClassId;
                ratingFromDb.FinancialStatementId = ratingDTO.FinancialStatementId;
                ratingFromDb.Analyst = ratingDTO.Analyst;
                ratingFromDb.Comment = ratingDTO.Comment;
                ratingFromDb.DateStart = ratingDTO.DateStart;
                ratingFromDb.IsConservative = ratingDTO.IsConservative;
                var updatedRating = _db.RatingInternals.Update(ratingFromDb);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<RatingInternal, RatingInternalDTO>(updatedRating.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
