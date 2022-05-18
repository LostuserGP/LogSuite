using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.Business.Repositories.IRepository;
using LogSuite.DataAccess;
using LogSuite.DataAccess.DailyReview;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.DailyReview.References
{
    public class GisCountryAddonRepository : IGisCountryAddonRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisCountryAddonRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GisCountryAddonDto> Create(GisCountryAddonDto dto)
        {
            var toDb = _mapper.Map<GisCountryAddonDto, GisCountryAddon>(dto);
            var result = await _db.GisCountryAddons.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GisCountryAddon, GisCountryAddonDto>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GisCountryAddons
                .Include(x => x.Values)
                .Include(x => x.Types)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (fromDb == null) return 0;
            _db.GisCountryAddons.Remove(fromDb);
            return await _db.SaveChangesAsync();
        }

        public async Task<GisCountryAddonDto> Get(int id)
        {
            var fromDb = await _db.GisCountryAddons
                .Include(x => x.Values)
                .Include(x => x.Types)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisCountryAddon, GisCountryAddonDto>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<GisCountryAddonDto>> GetAll()
        {
            var source = await _db.GisCountryAddons
                .OrderBy(x => x.Name)
                .ToListAsync();
            var entities = _mapper.Map<IEnumerable<GisCountryAddonDto>>(source);
            return entities;
        }

        public async Task<PagedList<GisCountryAddonDto>> GetPagedByGisCountryId(int gisCountryId, Params parameters)
        {
            var source = _db.GisCountryAddons
                .Where(x => x.GisCountryId == gisCountryId)
                .AsQueryable();
            // source = source.Search(parameters.Filter);
            // source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result =
                await PagedList<GisCountryAddon>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<GisCountryAddonDto>>(result);
            return new PagedList<GisCountryAddonDto>(entities, result.MetaData);
        }

        public async Task<GisCountryAddonDto> IsUnique(GisCountryAddonDto dto, int id = 0)
        {
            if (id > 0)
            {
                var fromDb = await _db.GisCountryAddons
                    .Where(x => x.GisCountryId == dto.GisCountryId && x.Name == dto.Name && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisCountryAddon, GisCountryAddonDto>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisCountryAddons
                    .Where(x => x.GisCountryId == dto.GisCountryId && x.Name == dto.Name)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisCountryAddon, GisCountryAddonDto>(fromDb);
                return result;
            }
        }

        public async Task<PagedList<GisCountryAddonDto>> GetPaged(Params parameters)
        {
            var source = _db.GisCountryAddons
                .AsQueryable();
            // source = source.Search(parameters.Filter);
            // source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result =
                await PagedList<GisCountryAddon>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<GisCountryAddonDto>>(result);
            return new PagedList<GisCountryAddonDto>(entities, result.MetaData);
        }

        public async Task<GisCountryAddonDto> Update(GisCountryAddonDto dto)
        {
            var fromDb = await _db.GisCountryAddons
                .FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.GisCountryAddons.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisCountryAddon, GisCountryAddonDto>(updated.Entity);
            return result;
        }
    }
}