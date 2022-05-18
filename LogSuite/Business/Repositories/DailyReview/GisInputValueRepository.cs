using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.DataAccess;
using LogSuite.DataAccess.DailyReview;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.DailyReview
{
    public class GisInputValueRepository : IGisInputValueRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisInputValueRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GisInputValueDTO> Create(GisInputValueDTO dto)
        {
            var toDb = _mapper.Map<GisInputValueDTO, GisInputValue>(dto);
            var result = await _db.GisInputValues.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GisInputValue, GisInputValueDTO>(result.Entity);
        }

        public async Task<int> Delete(long id)
        {
            var fromDb = await _db.GisInputValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (fromDb == null) return 0;
            _db.GisInputValues.Remove(fromDb);
            return await _db.SaveChangesAsync();
        }

        public Task<IEnumerable<GisInputValueDTO>> GetAll()
        {
            return null;
        }

        public async Task<GisInputValueDTO> Get(long id)
        {
            var fromDb = await _db.GisInputValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisInputValue, GisInputValueDTO>(fromDb);
            return dto;
        }

        public async Task<PagedList<GisInputValueDTO>> GetPagedByGisId(int gisId, Params parameters)
        {
            var source = _db.GisInputValues
                    .Include(x => x.RequestedValueTime)
                    .Include(x => x.AllocatedValueTime)
                    .Include(x => x.EstimatedValueTime)
                    .Include(x => x.FactValueTime)
                    .Where(x => x.GisId == gisId)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result = await PagedList<GisInputValue>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<GisInputValueDTO>>(result);
            return new PagedList<GisInputValueDTO>(entities, result.MetaData);
        }

        public async Task<GisInputValueDTO> GetOnDateByGisId(int gisId, DateOnly date)
        {
            var fromDb = await _db.GisInputValues
                    .Include(x => x.RequestedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.AllocatedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.EstimatedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.FactValueTime).ThenInclude(t => t.User)
                    .OrderByDescending(x => x.ReportDate)
                    .Where(x => x.GisId == gisId && x.ReportDate == date)
                    .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisInputValue, GisInputValueDTO>(fromDb);
            return dto;
        }

        public async Task<List<GisInputValueDTO>> GetOnDateRangeByGisId(int gisId, DateOnly startDate, DateOnly finishDate)
        {
            var source = await _db.GisInputValues
                    .Include(x => x.RequestedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.AllocatedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.EstimatedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.FactValueTime).ThenInclude(t => t.User)
                    .Where(x => x.GisId == gisId
                        && x.ReportDate >= startDate
                        && x.ReportDate <= finishDate)
                    .OrderByDescending(x => x.ReportDate)
                    .ToListAsync();
            var entities = _mapper.Map<List<GisInputValueDTO>>(source);
            return entities;
        }

        public async Task<GisInputValueDTO> IsUnique(GisInputValueDTO dto, long id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.GisInputValues
                    .Where(x => x.ReportDate == dto.ReportDate && x.GisId == dto.GisId)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisInputValue, GisInputValueDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisInputValues
                    .Where(x => x.ReportDate == dto.ReportDate && x.GisId == dto.GisId && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisInputValue, GisInputValueDTO>(fromDb);
                return result;
            }
        }

        public Task<PagedList<GisInputValueDTO>> GetPaged(Params parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<GisInputValueDTO> Update(GisInputValueDTO dto)
        {
            var fromDb = await _db.GisInputValues
                .Include(x => x.RequestedValueTime).ThenInclude(t => t.User)
                .Include(x => x.AllocatedValueTime).ThenInclude(t => t.User)
                .Include(x => x.EstimatedValueTime).ThenInclude(t => t.User)
                .Include(x => x.FactValueTime).ThenInclude(t => t.User)
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.GisInputValues.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisInputValue, GisInputValueDTO>(updated.Entity);
            return result;
        }
    }
}
