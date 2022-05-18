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
    public class GisAddonRepository : IGisAddonRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisAddonRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GisAddonDTO> Create(GisAddonDTO dto)
        {
            GisAddon toDb = _mapper.Map<GisAddonDTO, GisAddon>(dto);
            var result = await _db.GisAddons.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GisAddon, GisAddonDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GisAddons
                .Include(x => x.Values)
                .Include(x => x.Names)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (fromDb == null) return 0;
            _db.GisAddons.Remove(fromDb);
            return await _db.SaveChangesAsync();
        }

        public async Task<GisAddonDTO> Get(int id)
        {
            var fromDb = await _db.GisAddons
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisAddon, GisAddonDTO>(fromDb);
            return dto;
        }

        public Task<IEnumerable<GisAddonDTO>> GetAll()
        {
            var entities = _db.GisAddons.Include(x => x.Gis);
            var dtos = _mapper.Map<IEnumerable<GisAddon>, IEnumerable<GisAddonDTO>>(entities);
            return Task.FromResult(dtos);
        }

        public Task<IEnumerable<GisAddonDTO>> GetAllByGisId(int gisId)
        {
            var entities = _db.GisAddons.Include(x => x.Gis).Where(x => x.GisId == gisId);
            var dtos = _mapper.Map<IEnumerable<GisAddon>, IEnumerable<GisAddonDTO>>(entities);
            return Task.FromResult(dtos);
        }

        public async Task<PagedList<GisAddonDTO>> GetPagedByGisId(int gisId, Params parameters)
        {
            var source = _db.GisAddons
                .Include(x => x.Names)
                .Where(x => x.GisId == gisId)
                .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result = await PagedList<GisAddon>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<GisAddonDTO>>(result);
            return new PagedList<GisAddonDTO>(entities, result.MetaData);
        }

        public async Task<PagedList<GisAddonDTO>> GetPaged(Params parameters)
        {
            var source = _db.GisAddons
                .Include(x => x.Names)
                .Include(x => x.Gis)
                .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result = await PagedList<GisAddon>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<GisAddonDTO>>(result);
            return new PagedList<GisAddonDTO>(entities, result.MetaData);
        }

        public async Task<GisAddonDTO> IsUnique(GisAddonDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.GisAddons
                    .Where(x => x.Name.ToLower().Equals(dto.Name.ToLower()))
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisAddon, GisAddonDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisAddons
                    .Where(x => x.Name.ToLower().Equals(dto.Name.ToLower()) && x.Id != dto.Id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisAddon, GisAddonDTO>(fromDb);
                return result;
            }
        }

        public async Task<GisAddonDTO> Update(GisAddonDTO dto)
        {
            var fromDb = await _db.GisAddons
                .FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.GisAddons.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisAddon, GisAddonDTO>(updated.Entity);
            return result;
        }
    }
}