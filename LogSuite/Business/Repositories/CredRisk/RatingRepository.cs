using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.Business.Repositories.IRepository.References;
using LogSuite.DataAccess;
using LogSuite.DataAccess.CredRisk;
using LogSuite.Shared;
using LogSuite.Shared.Models.CredRisk;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.References
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
            var toDb = _mapper.Map<RatingDTO, Rating>(dto);
            var result = await _db.Ratings.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<Rating, RatingDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.Ratings.FindAsync(id);
            if (fromDb == null) return 0;
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

        public async Task<RatingDTO> Get(int id)
        {
            var fromDb = await _db.Ratings.FindAsync(id);
            var dto = _mapper.Map<Rating, RatingDTO>(fromDb);
            return dto;
        }

        public Task<IEnumerable<RatingDTO>> GetAll()
        {
            try
            {
                var entities = _db.Ratings;
                var dtos = _mapper.Map<IEnumerable<Rating>, IEnumerable<RatingDTO>>(entities);
                return Task.FromResult(dtos);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IEnumerable<RatingDTO>>(null);
            }
        }

        public async Task<RatingDTO> IsUnique(RatingDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.Ratings
                        .FirstOrDefaultAsync(x => string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase));
                    var result = _mapper.Map<Rating, RatingDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.Ratings
                        .FirstOrDefaultAsync(x => (string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase))
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

        public async Task<PagedList<RatingDTO>> GetPaged(Params parameters)
        {
            var source = _db.Ratings
                .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result = await PagedList<Rating>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<RatingDTO>>(result);

            return new PagedList<RatingDTO>(entities, result.MetaData);
        }

        public async Task<RatingDTO> Update(RatingDTO dto)
        {
            var fromDb = await _db.Ratings.FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.Ratings.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<Rating, RatingDTO>(updated.Entity);
            return result;
        }
    }
}