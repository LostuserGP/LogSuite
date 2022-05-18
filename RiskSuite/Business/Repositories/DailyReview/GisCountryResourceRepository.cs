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
using LogSuite.Business.Repositories.IRepository;

namespace Business.Repositories
{
    public class GisCountryResourceRepository : IGisCountryResourceRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisCountryResourceRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<GisCountryResourceDTO> Create(GisCountryResourceDTO dto)
        {
            GisCountryResource toDb = _mapper.Map<GisCountryResourceDTO, GisCountryResource>(dto);
            var result = await _db.GisCountryResources.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GisCountryResource, GisCountryResourceDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GisCountryResources
                .FindAsync(id);
            if (fromDb != null)
            {
                _db.GisCountryResources.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<GisCountryResourceDTO> Get(int id)
        {
            var fromDb = await _db.GisCountryResources
                .FindAsync(id);
            var dto = _mapper.Map<GisCountryResource, GisCountryResourceDTO>(fromDb);
            return dto;
        }

        public async Task<PagedList<GisCountryResourceDTO>> GetPagedByGisCountryId(int gisCountryId, Params parameters)
        {
            var source = _db.GisCountryResources
                    .Where(x => x.GisCountryId == gisCountryId)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisCountryResource>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisCountryResourceDTO>>(result);
            return new PagedList<GisCountryResourceDTO>(entities, result.MetaData);
        }

        public async Task<GisCountryResourceDTO> GetOnDateByGisCountryId(int gisCountryId, DateTime date)
        {
            var fromDb = await _db.GisCountryResources
                .Where(x => x.GisCountryId == gisCountryId && x.Month.Date == date.Date)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisCountryResource, GisCountryResourceDTO>(fromDb);
            return dto;
        }

        public async Task<GisCountryResourceDTO> IsUnique(GisCountryResourceDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.GisCountryResources
                    .Where(x => x.Month == dto.Month && x.GisCountryId == dto.GisCountryId)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisCountryResource, GisCountryResourceDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisCountryResources
                    .Where(x => x.Month == dto.Month && x.GisCountryId == dto.GisCountryId && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisCountryResource, GisCountryResourceDTO>(fromDb);
                return result;
            }
        }

        public async Task<object> Update(GisCountryResourceDTO dto)
        {
            var fromDb = await _db.GisCountryResources
                .FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            var updated = _db.GisCountryResources.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisCountryResource, GisCountryResourceDTO>(updated.Entity);
            return result;
        }
    }
}