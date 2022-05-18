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
    public class GisCountryValueRepository : IGisCountryValueRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisCountryValueRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<GisCountryValueDTO> Create(GisCountryValueDTO dto)
        {
            var toDb = _mapper.Map<GisCountryValueDTO, GisCountryValue>(dto);
            var result = await _db.GisCountryValues.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GisCountryValue, GisCountryValueDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GisCountryValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (fromDb == null) return 0;
            _db.GisCountryValues.Remove(fromDb);
            return await _db.SaveChangesAsync();
        }

        public Task<IEnumerable<GisCountryValueDTO>> GetAll()
        {
            return null;
        }

        public async Task<GisCountryValueDTO> Get(int id)
        {
            var fromDb = await _db.GisCountryValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisCountryValue, GisCountryValueDTO>(fromDb);
            return dto;
        }

        public async Task<PagedList<GisCountryValueDTO>> GetPagedByGisCountryId(int gisCountryId, Params parameters)
        {
            var source = _db.GisCountryValues
                //.Include(x => x.RequestedValueTime).ThenInclude(v => v.User)
                //.Include(x => x.AllocatedValueTime).ThenInclude(v => v.User)
                //.Include(x => x.EstimatedValueTime).ThenInclude(v => v.User)
                //.Include(x => x.FactValueTime).ThenInclude(v => v.User)
                .Where(x => x.GisCountryId == gisCountryId)
                .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisCountryValue>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisCountryValueDTO>>(result);
            return new PagedList<GisCountryValueDTO>(entities, result.MetaData);
        }

        public async Task<GisCountryValueDTO> GetOnDateByGisCountryId(int gisCountryId, DateOnly date)
        {
            var fromDb = await _db.GisCountryValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.GisCountryId == gisCountryId && x.ReportDate == date)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisCountryValue, GisCountryValueDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<GisCountryValueDTO>> GetOnDateRangeByGisCountryId(int gisCountryId, DateOnly startDate, DateOnly finishDate)
        {
            var result = await _db.GisCountryValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.GisCountryId == gisCountryId
                        && x.ReportDate >= startDate
                        && x.ReportDate <= finishDate)
                .OrderByDescending(x => x.ReportDate)
                .ToListAsync();
            var entities = _mapper.Map<List<GisCountryValueDTO>>(result);
            return entities;
        }

        public async Task<GisCountryValueDTO> IsUnique(GisCountryValueDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.GisCountryValues
                    .Where(x => x.ReportDate == dto.ReportDate && x.GisCountryId == dto.GisCountryId)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisCountryValue, GisCountryValueDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisCountryValues
                    .Where(x => x.ReportDate == dto.ReportDate && x.GisCountryId == dto.GisCountryId && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisCountryValue, GisCountryValueDTO>(fromDb);
                return result;
            }
        }

        public Task<PagedList<GisCountryValueDTO>> GetPaged(Params parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<GisCountryValueDTO> Update(GisCountryValueDTO dto)
        {
            var fromDb = await _db.GisCountryValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.GisCountryValues.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisCountryValue, GisCountryValueDTO>(updated.Entity);
            return result;
        }
    }
}