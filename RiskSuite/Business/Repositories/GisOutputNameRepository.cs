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
    public class GisOutputNameRepository : IGisOutputNameRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisOutputNameRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GisOutputNameDTO> Create(GisOutputNameDTO dto)
        {
            GisOutputName toDb = _mapper.Map<GisOutputNameDTO, GisOutputName>(dto);
            var result = await _db.GisOutputNames.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GisOutputName, GisOutputNameDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GisOutputNames
                .FindAsync(id);
            if (fromDb != null)
            {
                _db.GisOutputNames.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<GisOutputNameDTO> Get(int id)
        {
            var fromDb = await _db.GisOutputNames
                .FindAsync(id);
            var dto = _mapper.Map<GisOutputName, GisOutputNameDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<GisOutputNameDTO>> GetAll()
        {
            var entities = _db.GisOutputNames;
            IEnumerable<GisOutputNameDTO> dtos = _mapper.Map<IEnumerable<GisOutputName>, IEnumerable<GisOutputNameDTO>>(entities);
            return dtos;
        }

        public async Task<IEnumerable<GisOutputNameDTO>> GetAllByGisId(int gisId)
        {
            var entities = _db.GisOutputNames.Where(x => x.GisId == gisId);
            IEnumerable<GisOutputNameDTO> dtos = _mapper.Map<IEnumerable<GisOutputName>, IEnumerable<GisOutputNameDTO>>(entities);
            return dtos;
        }

        public async Task<PagedList<GisOutputNameDTO>> GetPaged(Params parameters)
        {
            var source = _db.GisOutputNames
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisOutputName>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisOutputNameDTO>>(result);
            return new PagedList<GisOutputNameDTO>(entities, result.MetaData);
        }

        public async Task<PagedList<GisOutputNameDTO>> GetPagedByGisId(int gisId, Params parameters)
        {
            var source = _db.GisOutputNames
                    .Where(x => x.GisId == gisId)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisOutputName>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisOutputNameDTO>>(result);
            return new PagedList<GisOutputNameDTO>(entities, result.MetaData);
        }

        public async Task<GisOutputNameDTO> IsUnique(GisOutputNameDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.GisOutputNames
                    .Where(x => x.Name.ToLower().Equals(dto.Name.ToLower()) && x.GisId == dto.GisId)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisOutputName, GisOutputNameDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisOutputNames
                    .Where(x => x.Name.ToLower().Equals(dto.Name.ToLower()) && x.GisId == dto.GisId && x.Id != dto.Id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisOutputName, GisOutputNameDTO>(fromDb);
                return result;
            }
        }

        public async Task<GisOutputNameDTO> Update(GisOutputNameDTO dto)
        {
            var fromDb = await _db.GisOutputNames
                .FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            var updated = _db.GisOutputNames.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisOutputName, GisOutputNameDTO>(updated.Entity);
            return result;
        }
    }
}
