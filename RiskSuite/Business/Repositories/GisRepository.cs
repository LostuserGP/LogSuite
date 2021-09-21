using AutoMapper;
using Business.Repositories.IRepository;
using LogSuite.DataAccess;
using LogSuite.DataAccess.DailyReview;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Business.Repositories
{
    public class GisRepository : IGisRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<GisDTO> Create(GisDTO dto)
        {
            Gis toDb = _mapper.Map<GisDTO, Gis>(dto);
            var result = await _db.Gises.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<Gis, GisDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.Gises
                .Include(x => x.Names)
                .Include(x => x.Addons)
                .Include(x => x.GisInputNames)
                .Include(x => x.GisOutputNames)
                .Include(x => x.GisInputValues)
                .Include(x => x.GisOutputValues)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (fromDb != null)
            {
                var parent = await _db.GisCountries
                    .Where(x => x.GisId == id)
                    .AnyAsync();
                if (parent)
                {
                    return -1;
                }
                else
                {
                    _db.Gises.Remove(fromDb);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<GisDTO> Get(int id)
        {
            var fromDb = await _db.Gises
                .Include(x => x.Names)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<Gis, GisDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<GisDTO>> GetAll()
        {
            var entities = _db.Gises
                .Include(x => x.Names)
                .Include(x => x.GisInputNames)
                .Include(x => x.GisOutputNames)
                .Include(x => x.Addons).ThenInclude(a => a.Names)
                .Include(x => x.Countries.OrderBy(x => x.Country.Name)).ThenInclude(gc => gc.Country).ThenInclude(n => n.Names)
                .OrderBy(x => x.Name);
            IEnumerable<GisDTO> dtos = _mapper.Map<IEnumerable<Gis>, IEnumerable<GisDTO>>(entities);
            return dtos;
        }

        public async Task<PagedList<GisDTO>> GetPaged(Params parameters)
        {
            var source = _db.Gises
                    .Include(x => x.Names)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<Gis>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisDTO>>(result);

            return new PagedList<GisDTO>(entities, result.MetaData);
        }

        public async Task<GisDTO> IsUnique(GisDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.Gises.Include(x => x.Names)
                    .Where(x => x.Names.Where(n => dto.Name.ToLower().Equals(n.Name.ToLower())).Any()
                        || x.Name.ToLower() == dto.Name.ToLower())
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<Gis, GisDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.Gises.Include(x => x.Names)
                    .Where(x => (x.Names.Where(n => dto.Name.ToLower().Equals(n.Name.ToLower())).Any()
                        || x.Name.ToLower() == dto.Name.ToLower())
                        && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<Gis, GisDTO>(fromDb);
                return result;
            }
        }

        public async Task<GisDTO> Update(GisDTO dto)
        {
            var fromDb = await _db.Gises
                .FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            var updated = _db.Gises.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<Gis, GisDTO>(updated.Entity);
            return result;
        }
    }
}