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
    public class RatingAgencyRepository : IRatingAgencyRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RatingAgencyRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<RatingAgencyDTO> Create(RatingAgencyDTO dto)
        {
            var toDb = _mapper.Map<RatingAgencyDTO, RatingAgency>(dto);
            var result = await _db.RatingAgencies.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<RatingAgency, RatingAgencyDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.RatingAgencies.FindAsync(id);
            if (fromDb == null) return 0;
            var parent = await _db.RatingExternals
                .Where(x => x.RatingAgencyId == id)
                .AnyAsync();
            if (parent)
            {
                return -1;
            }
            else
            {
                _db.RatingAgencies.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
        }

        public async Task<RatingAgencyDTO> Get(int id)
        {
            var fromDb = await _db.RatingAgencies.FindAsync(id);
            var dto = _mapper.Map<RatingAgency, RatingAgencyDTO>(fromDb);
            return dto;
        }

        public Task<IEnumerable<RatingAgencyDTO>> GetAll()
        {
            try
            {
                var entities = _db.RatingAgencies;
                var dtos = _mapper.Map<IEnumerable<RatingAgency>, IEnumerable<RatingAgencyDTO>>(entities);
                return Task.FromResult(dtos);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IEnumerable<RatingAgencyDTO>>(null);
            }
        }

        public async Task<RatingAgencyDTO> IsUnique(RatingAgencyDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.RatingAgencies
                        .FirstOrDefaultAsync(x => x.Name.ToLower() == dto.Name.ToLower());
                    var result = _mapper.Map<RatingAgency, RatingAgencyDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.RatingAgencies
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == dto.Name.ToLower())
                        && x.Id != id);
                    var result = _mapper.Map<RatingAgency, RatingAgencyDTO>(fromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PagedList<RatingAgencyDTO>> GetPaged(Params parameters)
        {
            var source = _db.RatingAgencies
                .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<RatingAgency>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<RatingAgencyDTO>>(result);

            return new PagedList<RatingAgencyDTO>(entities, result.MetaData);
        }

        public async Task<RatingAgencyDTO> Update(RatingAgencyDTO dto)
        {
            var fromDb = await _db.RatingAgencies.FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.RatingAgencies.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<RatingAgency, RatingAgencyDTO>(updated.Entity);
            return result;
        }
    }
}