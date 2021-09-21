using AutoMapper;
using Business.Repositories.IRepository;
using LogSuite.Business;
using LogSuite.Business.Repositories;
using LogSuite.Business.Repositories.References;
using LogSuite.DataAccess;
using LogSuite.DataAccess.DailyReview;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class GisNameRepository : IGisNameRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisNameRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<GisNameDTO> Create(GisNameDTO dto)
        {
            GisName toDb = _mapper.Map<GisNameDTO, GisName>(dto);
            var result = await _db.GisNames.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GisName, GisNameDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GisNames
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (fromDb != null)
            {
                _db.GisNames.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<GisNameDTO> Get(int id)
        {
            var fromDb = await _db.GisNames.FindAsync(id);
            var dto = _mapper.Map<GisName, GisNameDTO>(fromDb);
            return dto;
        }

        public async Task<PagedList<GisNameDTO>> GetByGisId(int gisId, Params parameters)
        {
            var source = _db.GisNames
                    .Where(x => x.GisId == gisId)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisName>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisNameDTO>>(result);

            return new PagedList<GisNameDTO>(entities, result.MetaData);
        }

        public async Task<GisNameDTO> IsUnique(GisNameDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.GisNames
                    .Where(x => x.Name.ToLower().Equals(dto.Name.ToLower()))
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisName, GisNameDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisNames
                    .Where(x => x.Name.ToLower().Equals(dto.Name.ToLower()) && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisName, GisNameDTO>(fromDb);
                return result;
            }
        }

        public async Task<GisNameDTO> Update(GisNameDTO dto)
        {
            var fromDb = await _db.GisNames
                .FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            var updated = _db.GisNames.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisName, GisNameDTO>(updated.Entity);
            return result;
        }
    }
}