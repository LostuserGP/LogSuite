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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class GisAddonValueRepository : IGisAddonValueRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisAddonValueRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<GisAddonValueDTO> Create(GisAddonValueDTO dto)
        {
            GisAddonValue toDb = _mapper.Map<GisAddonValueDTO, GisAddonValue>(dto);
            var result = await _db.GisAddonValues.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GisAddonValue, GisAddonValueDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GisAddonValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (fromDb != null)
            {
                _db.GisAddonValues.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<GisAddonValueDTO> Get(int id)
        {
            var fromDb = await _db.GisAddonValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisAddonValue, GisAddonValueDTO>(fromDb);
            return dto;
        }

        public async Task<GisAddonValueDTO> GetOnDateByGisAddonId(int gisAddonId, DateTime date)
        {
            var fromDb = await _db.GisAddonValues
                    .Include(x => x.RequestedValueTime)
                    .Include(x => x.AllocatedValueTime)
                    .Include(x => x.EstimatedValueTime)
                    .Include(x => x.FactValueTime)
                    .OrderByDescending(x => x.DateReport)
                    .Where(x => x.GisAddonId == gisAddonId && x.DateReport.Date == date.Date)
                    .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisAddonValue, GisAddonValueDTO>(fromDb);
            return dto;
        }

        public async Task<List<GisAddonValueDTO>> GetOnDateRangeByGisAddonId(int gisAddonId, DateTime dateStart, DateTime dateEnd)
        {
            var source = await _db.GisAddonValues
                    .Include(x => x.RequestedValueTime)
                    .Include(x => x.AllocatedValueTime)
                    .Include(x => x.EstimatedValueTime)
                    .Include(x => x.FactValueTime)
                    .Where(x => x.GisAddonId == gisAddonId
                        && x.DateReport.Date >= dateStart.Date
                        && x.DateReport.Date <= dateEnd.Date)
                    .OrderByDescending(x => x.DateReport)
                    .ToListAsync();
            var entities = _mapper.Map<List<GisAddonValueDTO>>(source);
            return entities;
        }

        public async Task<PagedList<GisAddonValueDTO>> GetPagedByGisAddonId(int gisAddonId, Params parameters)
        {
            var source = _db.GisAddonValues
                    .Include(x => x.RequestedValueTime)
                    .Include(x => x.AllocatedValueTime)
                    .Include(x => x.EstimatedValueTime)
                    .Include(x => x.FactValueTime)
                    .Where(x => x.GisAddonId == gisAddonId)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisAddonValue>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisAddonValueDTO>>(result);
            return new PagedList<GisAddonValueDTO>(entities, result.MetaData);
        }

        public async Task<GisAddonValueDTO> IsUnique(GisAddonValueDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.GisAddonValues
                    .Where(x => x.DateReport == dto.DateReport && x.GisAddonId == dto.GisAddonId)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisAddonValue, GisAddonValueDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisAddonValues
                    .Where(x => x.DateReport == dto.DateReport && x.GisAddonId == dto.GisAddonId && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisAddonValue, GisAddonValueDTO>(fromDb);
                return result;
            }
        }

        public async Task<GisAddonValueDTO> Update(GisAddonValueDTO dto)
        {
            var fromDb = await _db.GisAddonValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();
            var toUpdate = _mapper.Map(dto, fromDb);
            var updated = _db.GisAddonValues.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisAddonValue, GisAddonValueDTO>(updated.Entity);
            return result;
        }
    }
}