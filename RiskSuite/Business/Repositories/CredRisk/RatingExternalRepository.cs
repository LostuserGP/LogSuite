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
using LogSuite.Shared.Models.CredRisk;

namespace Business.Repositories
{
    public class RatingExternalRepository : IRatingExternalRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RatingExternalRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<RatingExternalDTO> Create(RatingExternalDTO ratingDTO)
        {
            RatingExternal rating = _mapper.Map<RatingExternalDTO, RatingExternal>(ratingDTO);
            var newRating = await _db.RatingExternals.AddAsync(rating);
            await _db.SaveChangesAsync();
            return _mapper.Map<RatingExternal, RatingExternalDTO>(newRating.Entity);
        }

        public async Task<int> Delete(int ratingId)
        {
            var rating = await _db.RatingExternals
                .Where(x => x.Id == ratingId)
                .FirstOrDefaultAsync();
            if (rating != null)
            {
                _db.RatingExternals.Remove(rating);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<RatingExternalDTO> Get(int ratingId)
        {
            var rating = await _db.RatingExternals
                .Include(x => x.Rating)
                .Include(x => x.RatingAgency)
                .FirstOrDefaultAsync(x => x.Id == ratingId);
            var ratingDTO = _mapper.Map<RatingExternal, RatingExternalDTO>(rating);
            return ratingDTO;
        }

        public async Task<IEnumerable<RatingExternalDTO>> GetAll(int counterpartyId)
        {
            try
            {
                var ratings = await _db.RatingExternals
                    .Include(x => x.Rating)
                    .Include(x => x.RatingAgency)
                    .Where(x => x.CounterpartyId == counterpartyId)
                    .OrderByDescending(x => x.DateStart)
                    .ToListAsync();
                IEnumerable<RatingExternalDTO> ratingDTOs = _mapper.Map<IEnumerable<RatingExternal>, IEnumerable<RatingExternalDTO>>(ratings);
                return ratingDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RatingExternalDTO> IsUnique(RatingExternalDTO ratingDTO, int ratingId = 0)
        {
            try
            {
                if (ratingId == 0)
                {
                    var ratingFromDb = await _db.RatingExternals
                        .FirstOrDefaultAsync(x => x.CounterpartyId == ratingDTO.CounterpartyId
                        && x.DateStart == ratingDTO.DateStart && x.RatingAgencyId == ratingDTO.RatingAgency.Id);
                    var result = _mapper.Map<RatingExternal, RatingExternalDTO>(ratingFromDb);
                    return result;
                }
                else
                {
                    var ratingFromDb = await _db.RatingExternals
                        .FirstOrDefaultAsync(x => x.CounterpartyId == ratingDTO.CounterpartyId
                        && x.DateStart == ratingDTO.DateStart && x.RatingAgencyId == ratingDTO.RatingAgency.Id
                        && x.Id != ratingId);
                    var result = _mapper.Map<RatingExternal, RatingExternalDTO>(ratingFromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RatingExternalDTO> Update(RatingExternalDTO ratingDTO)
        {
            try
            {
                RatingExternal ratingFromDb = await _db.RatingExternals.FindAsync(ratingDTO.Id);
                ratingFromDb.RatingAgencyId = ratingDTO.RatingAgencyId;
                ratingFromDb.RatingId = ratingDTO.RatingId;
                ratingFromDb.DateStart = ratingDTO.DateStart;
                var updatedRating = _db.RatingExternals.Update(ratingFromDb);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<RatingExternal, RatingExternalDTO>(updatedRating.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
