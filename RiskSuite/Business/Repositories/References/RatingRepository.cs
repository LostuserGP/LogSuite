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
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RatingRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<RatingDTO> Create(RatingDTO dto)
        {
            Rating toDb = _mapper.Map<RatingDTO, Rating>(dto);
            var result = await _db.Ratings.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<Rating, RatingDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.Ratings.FindAsync(id);
            if (fromDb != null)
            {
                var parent = await _db.RatingExternals
                    .Where(x => x.RatingId == id)
                    .AnyAsync();
                if (parent)
                {
                    return -1;
                }
                var parent2 = await _db.RatingInternals
                    .Where(x => x.RatingId == id)
                    .AnyAsync();
                if (parent2)
                {
                    return -1;
                }
                _db.Ratings.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<RatingDTO> Get(int id)
        {
            var fromDb = await _db.Ratings.FindAsync(id);
            var dto = _mapper.Map<Rating, RatingDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<RatingDTO>> GetAll()
        {
            try
            {
                var entities = _db.Ratings;
                IEnumerable<RatingDTO> dtos = _mapper.Map<IEnumerable<Rating>, IEnumerable<RatingDTO>>(entities);
                return dtos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RatingDTO> IsUnique(RatingDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.Ratings
                        .FirstOrDefaultAsync(x => x.Name.ToLower() == dto.Name.ToLower());
                    var result = _mapper.Map<Rating, RatingDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.Ratings
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == dto.Name.ToLower())
                        && x.Id != id);
                    var result = _mapper.Map<Rating, RatingDTO>(fromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RatingDTO> Update(RatingDTO dto)
        {
            try
            {
                var fromDb = await _db.Ratings.FindAsync(dto.Id);
                var toUpdate = _mapper.Map(dto, fromDb);
                var updated = _db.Ratings.Update(toUpdate);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<Rating, RatingDTO>(updated.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}