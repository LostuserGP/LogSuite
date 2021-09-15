using AutoMapper;
using Business.Repositories.IRepository;
using LogSuite.Business;
using LogSuite.Business.Repositories;
using LogSuite.Business.Repositories.References;
using LogSuite.DataAccess;
using LogSuite.DataAccess.Operativka;
using LogSuite.Shared;
using LogSuite.Shared.Models.Operativka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repositories
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
            //GisCountryValue toDb = new GisCountryValue()
            //{
            //    GisCountryId = dto.GisCountryId,
            //    DateReport = dto.DateReport,
            //    RequstedValue = dto.RequstedValue,
            //    RequestedValueTimeId = dto.RequestedValueTimeId,
            //    AllocatedValue = dto.AllocatedValue,
            //    AllocatedValueTimeId = dto.AllocatedValueTimeId,
            //    EstimatedValue = dto.EstimatedValue,
            //    EstimatedValueTimeId = dto.EstimatedValueTimeId,
            //    FactValue = dto.FactValue,
            //    FactValueTimeId = dto.FactValueTimeId
            //};
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
            if (fromDb != null)
            {
                _db.GisCountryValues.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
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
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.GisCountryId == gisCountryId)
                .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisCountryValue>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisCountryValueDTO>>(result);
            return new PagedList<GisCountryValueDTO>(entities, result.MetaData);
        }

        public async Task<GisCountryValueDTO> GetOnDateByGisCountryId(int gisCountryId, DateTime date)
        {
            var fromDb = await _db.GisCountryValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.GisCountryId == gisCountryId && x.DateReport.Date == date.Date)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisCountryValue, GisCountryValueDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<GisCountryValueDTO>> GetOnDateRangeByGisCountryId(int gisCountryId, DateTime dateStart, DateTime dateEnd)
        {
            var result = await _db.GisCountryValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.GisCountryId == gisCountryId
                        && x.DateReport.Date >= dateStart.Date
                        && x.DateReport.Date <= dateEnd.Date)
                .OrderByDescending(x => x.DateReport)
                .ToListAsync();
            var entities = _mapper.Map<List<GisCountryValueDTO>>(result);
            return entities;
        }

        public async Task<GisCountryValueDTO> IsUnique(GisCountryValueDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.GisCountryValues
                    .Where(x => x.DateReport == dto.DateReport && x.GisCountryId == dto.GisCountryId)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisCountryValue, GisCountryValueDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisCountryValues
                    .Where(x => x.DateReport == dto.DateReport && x.GisCountryId == dto.GisCountryId && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisCountryValue, GisCountryValueDTO>(fromDb);
                return result;
            }
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
            var updated = _db.GisCountryValues.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisCountryValue, GisCountryValueDTO>(updated.Entity);
            return result;
        }
    }
}