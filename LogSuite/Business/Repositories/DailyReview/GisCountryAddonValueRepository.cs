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
    public class GisCountryAddonValueRepository : IGisCountryAddonValueRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisCountryAddonValueRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<GisCountryAddonValueDto> Create(GisCountryAddonValueDto dto)
        {
            var toDb = _mapper.Map<GisCountryAddonValueDto, GisCountryAddonValue>(dto);
            var result = await _db.GisCountryAddonValues.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GisCountryAddonValue, GisCountryAddonValueDto>(result.Entity);
        }

        public async Task<int> Delete(long id)
        {
            var fromDb = await _db.GisCountryAddonValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (fromDb == null) return 0;
            _db.GisCountryAddonValues.Remove(fromDb);
            return await _db.SaveChangesAsync();
        }

        public Task<IEnumerable<GisCountryAddonValueDto>> GetAll()
        {
            return null;
        }

        public async Task<GisCountryAddonValueDto> Get(long id)
        {
            var fromDb = await _db.GisCountryAddonValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisCountryAddonValue, GisCountryAddonValueDto>(fromDb);
            return dto;
        }

        public async Task<PagedList<GisCountryAddonValueDto>> GetPagedByGisCountryAddonId(int gisCountryAddonId, Params parameters)
        {
            var source = _db.GisCountryAddonValues
                //.Include(x => x.RequestedValueTime).ThenInclude(v => v.User)
                //.Include(x => x.AllocatedValueTime).ThenInclude(v => v.User)
                //.Include(x => x.EstimatedValueTime).ThenInclude(v => v.User)
                //.Include(x => x.FactValueTime).ThenInclude(v => v.User)
                .Where(x => x.GisCountryAddonId == gisCountryAddonId)
                .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result = await PagedList<GisCountryAddonValue>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<GisCountryAddonValueDto>>(result);
            return new PagedList<GisCountryAddonValueDto>(entities, result.MetaData);
        }

        public async Task<GisCountryAddonValueDto> GetOnDateByGisCountryAddonId(int gisCountryAddonId, DateOnly date)
        {
            var fromDb = await _db.GisCountryAddonValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.GisCountryAddonId == gisCountryAddonId && x.ReportDate == date)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisCountryAddonValue, GisCountryAddonValueDto>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<GisCountryAddonValueDto>> GetOnDateRangeByGisCountryAddonId(int gisCountryAddonId, DateOnly startDate, DateOnly finishDate)
        {
            var result = await _db.GisCountryAddonValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.GisCountryAddonId == gisCountryAddonId
                        && x.ReportDate >= startDate
                        && x.ReportDate <= finishDate)
                .OrderByDescending(x => x.ReportDate)
                .ToListAsync();
            var entities = _mapper.Map<List<GisCountryAddonValueDto>>(result);
            return entities;
        }

        public async Task<GisCountryAddonValueDto> IsUnique(GisCountryAddonValueDto dto, long id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.GisCountryAddonValues
                    .Where(x => x.ReportDate == dto.ReportDate && x.GisCountryAddonId == dto.GisCountryAddonId)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisCountryAddonValue, GisCountryAddonValueDto>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisCountryAddonValues
                    .Where(x => x.ReportDate == dto.ReportDate && x.GisCountryAddonId == dto.GisCountryAddonId && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisCountryAddonValue, GisCountryAddonValueDto>(fromDb);
                return result;
            }
        }

        public Task<PagedList<GisCountryAddonValueDto>> GetPaged(Params parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<GisCountryAddonValueDto> Update(GisCountryAddonValueDto dto)
        {
            var fromDb = await _db.GisCountryAddonValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.GisCountryAddonValues.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisCountryAddonValue, GisCountryAddonValueDto>(updated.Entity);
            return result;
        }
    }
}