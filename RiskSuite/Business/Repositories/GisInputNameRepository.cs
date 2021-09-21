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
    public class GisInputNameRepository : IGisInputNameRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisInputNameRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GisInputNameDTO> Create(GisInputNameDTO dto)
        {
            GisInputName toDb = _mapper.Map<GisInputNameDTO, GisInputName>(dto);
            var result = await _db.GisInputNames.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GisInputName, GisInputNameDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GisInputNames
                .FindAsync(id);
            if (fromDb != null)
            {
                _db.GisInputNames.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<GisInputNameDTO> Get(int id)
        {
            var fromDb = await _db.GisInputNames
                .FindAsync(id);
            var dto = _mapper.Map<GisInputName, GisInputNameDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<GisInputNameDTO>> GetAllByGisId(int gisId)
        {
            var entities = _db.GisInputNames.Where(x => x.GisId == gisId);
            IEnumerable<GisInputNameDTO> dtos = _mapper.Map<IEnumerable<GisInputName>, IEnumerable<GisInputNameDTO>>(entities);
            return dtos;
        }

        public async Task<PagedList<GisInputNameDTO>> GetPaged(Params parameters)
        {
            var source = _db.GisInputNames
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisInputName>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisInputNameDTO>>(result);
            return new PagedList<GisInputNameDTO>(entities, result.MetaData);
        }

        public async Task<PagedList<GisInputNameDTO>> GetPagedByGisId(int gisId, Params parameters)
        {
            var source = _db.GisInputNames
                    .Where(x => x.GisId == gisId)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisInputName>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisInputNameDTO>>(result);
            return new PagedList<GisInputNameDTO>(entities, result.MetaData);
        }

        public async Task<GisInputNameDTO> IsUnique(GisInputNameDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.GisInputNames
                    .Where(x => x.Name.ToLower().Equals(dto.Name.ToLower()) && x.GisId == dto.GisId)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisInputName, GisInputNameDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisInputNames
                    .Where(x => x.Name.ToLower().Equals(dto.Name.ToLower()) && x.GisId == dto.GisId && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisInputName, GisInputNameDTO>(fromDb);
                return result;
            }
        }

        public async Task<GisInputNameDTO> Update(GisInputNameDTO dto)
        {
            var fromDb = await _db.GisInputNames
                .FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            var updated = _db.GisInputNames.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisInputName, GisInputNameDTO>(updated.Entity);
            return result;
        }
    }
}
