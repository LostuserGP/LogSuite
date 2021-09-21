using AutoMapper;
using Business.Repositories.IRepository;
using LogSuite.DataAccess;
using LogSuite.DataAccess.DailyReview;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSuite.Business.Repositories
{
    public class GisOutputValueRepository : IGisOutputValueRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisOutputValueRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GisOutputValueDTO> Create(GisOutputValueDTO dto)
        {
            GisOutputValue toDb = _mapper.Map<GisOutputValueDTO, GisOutputValue>(dto);
            var result = await _db.GisOutputValues.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GisOutputValue, GisOutputValueDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GisOutputValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (fromDb != null)
            {
                _db.GisOutputValues.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<GisOutputValueDTO> Get(int id)
        {
            var fromDb = await _db.GisOutputValues
                .Include(x => x.RequestedValueTime)
                .Include(x => x.AllocatedValueTime)
                .Include(x => x.EstimatedValueTime)
                .Include(x => x.FactValueTime)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisOutputValue, GisOutputValueDTO>(fromDb);
            return dto;
        }

        public async Task<PagedList<GisOutputValueDTO>> GetPagedByGisId(int gisId, Params parameters)
        {
            var source = _db.GisOutputValues
                    .Include(x => x.RequestedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.AllocatedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.EstimatedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.FactValueTime).ThenInclude(t => t.User)
                    .Where(x => x.GisId == gisId)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisOutputValue>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisOutputValueDTO>>(result);
            return new PagedList<GisOutputValueDTO>(entities, result.MetaData);
        }

        public async Task<GisOutputValueDTO> GetOnDateByGisId(int gisId, DateTime date)
        {
            var fromDb = await _db.GisOutputValues
                    .Include(x => x.RequestedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.AllocatedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.EstimatedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.FactValueTime).ThenInclude(t => t.User)
                    .OrderByDescending(x => x.DateReport)
                    .Where(x => x.GisId == gisId && x.DateReport.Date == date.Date)
                    .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisOutputValue, GisOutputValueDTO>(fromDb);
            return dto;
        }

        public async Task<List<GisOutputValueDTO>> GetOnDateRangeByGisId(int gisId, DateTime dateStart, DateTime dateEnd)
        {
            var source = await _db.GisOutputValues
                    .Include(x => x.RequestedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.AllocatedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.EstimatedValueTime).ThenInclude(t => t.User)
                    .Include(x => x.FactValueTime).ThenInclude(t => t.User)
                    .Where(x => x.GisId == gisId
                        && x.DateReport.Date >= dateStart.Date
                        && x.DateReport.Date <= dateEnd.Date)
                    .OrderByDescending(x => x.DateReport)
                    .ToListAsync();
            var entities = _mapper.Map<List<GisOutputValueDTO>>(source);
            return entities;
        }

        public async Task<GisOutputValueDTO> IsUnique(GisOutputValueDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.GisOutputValues
                    .Where(x => x.DateReport == dto.DateReport && x.GisId == dto.GisId)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisOutputValue, GisOutputValueDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisOutputValues
                    .Where(x => x.DateReport == dto.DateReport && x.GisId == dto.GisId && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisOutputValue, GisOutputValueDTO>(fromDb);
                return result;
            }
        }

        public async Task<GisOutputValueDTO> Update(GisOutputValueDTO dto)
        {
            var fromDb = await _db.GisOutputValues
                .Include(x => x.RequestedValueTime).ThenInclude(t => t.User)
                .Include(x => x.AllocatedValueTime).ThenInclude(t => t.User)
                .Include(x => x.EstimatedValueTime).ThenInclude(t => t.User)
                .Include(x => x.FactValueTime).ThenInclude(t => t.User)
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();
            var toUpdate = _mapper.Map(dto, fromDb);
            var updated = _db.GisOutputValues.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisOutputValue, GisOutputValueDTO>(updated.Entity);
            return result;
        }
    }
}
