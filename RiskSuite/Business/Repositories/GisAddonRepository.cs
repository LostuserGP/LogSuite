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
            if (fromDb != null)
            {
                _db.GisAddons.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<GisAddonDTO> Get(int id)
        {
            var fromDb = await _db.GisAddons
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisAddon, GisAddonDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<GisAddonDTO>> GetAll()
        {
            var entities = _db.GisAddons.Include(x => x.Gis);
            IEnumerable<GisAddonDTO> dtos = _mapper.Map<IEnumerable<GisAddon>, IEnumerable<GisAddonDTO>>(entities);
            return dtos;
        }

        public async Task<IEnumerable<GisAddonDTO>> GetAllByGisId(int gisId)
        {
            var entities = _db.GisAddons.Include(x => x.Gis).Where(x => x.GisId == gisId);
            IEnumerable<GisAddonDTO> dtos = _mapper.Map<IEnumerable<GisAddon>, IEnumerable<GisAddonDTO>>(entities);
            return dtos;
        }

        public async Task<PagedList<GisAddonDTO>> GetPagedByGisId(int gisId, Params parameters)
        {
            var source = _db.GisAddons
                    .Include(x => x.Names)
                    .Where(x => x.GisId == gisId)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisAddon>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
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
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisAddon>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
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
            var updated = _db.GisAddons.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisAddon, GisAddonDTO>(updated.Entity);
            return result;
        }
    }
}