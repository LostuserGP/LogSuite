using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.Business.Repositories.DailyReview.References;
using LogSuite.DataAccess;
using LogSuite.DataAccess.DailyReview;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.DailyReview
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
            var toDb = _mapper.Map<GisDTO, Gis>(dto);
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
            if (fromDb == null) return 0;
            {
                var parent = await _db.GisCountries
                    .Where(x => x.GisId == id)
                    .AnyAsync();
                if (parent)
                {
                    return -1;
                }

                _db.Gises.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
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

        public Task<IEnumerable<GisDTO>> GetAll()
        {
            var entities = _db.Gises
                .Include(x => x.Addons)
                .Include(x => x.Countries.OrderBy(x => x.Country.Name)).ThenInclude(gc => gc.Country)
                .OrderBy(x => x.Name).AsEnumerable();
            var dtos = _mapper.Map<IEnumerable<Gis>, IEnumerable<GisDTO>>(entities);
            return Task.FromResult(dtos);
        }

        public async Task<List<GisDTO>> GetOnDateRange(DateOnly startDate, DateOnly finishDate)
        {
            var gises = await _db.Gises
                .Include(x => x.Countries).ThenInclude(c => c.Country)
                .Include(x => x.Addons)
                .ToListAsync();
            foreach (var gis in gises)
            {
                foreach (var country in gis.Countries)
                {
                    country.Values = await _db.GisCountryValues
                        .Where(x => x.GisCountryId == country.Id && x.ReportDate >= startDate &&
                                    x.ReportDate <= finishDate)
                        .OrderBy(x => x.ReportDate)
                        .ToListAsync();
                    var firstDate = new DateOnly(startDate.Year, startDate.Month, 1);
                    var lastDate = new DateOnly(finishDate.Year, finishDate.Month, 1);
                    country.Resources = await _db.GisCountryResources
                        .Where(x => x.GisCountryId == country.Id && x.Month >= firstDate && x.Month <= lastDate)
                        .OrderBy(x => x.Month)
                        .ToListAsync();
                }

                foreach (var addon in gis.Addons)
                {
                    addon.Values = await _db.GisAddonValues
                        .Where(x => x.GisAddonId == addon.Id && x.ReportDate >= startDate && x.ReportDate <= finishDate)
                        .OrderBy(x => x.ReportDate)
                        .ToListAsync();
                }

                gis.GisInputValues = await _db.GisInputValues
                    .Where(x => x.GisId == gis.Id && x.ReportDate >= startDate && x.ReportDate <= finishDate)
                    .OrderBy(x => x.ReportDate)
                    .ToListAsync();
                gis.GisOutputValues = await _db.GisOutputValues
                    .Where(x => x.GisId == gis.Id && x.ReportDate >= startDate && x.ReportDate <= finishDate)
                    .OrderBy(x => x.ReportDate)
                    .ToListAsync();
            }

            var dtos = _mapper.Map<IEnumerable<Gis>, List<GisDTO>>(gises);
            return dtos;
        }

        public async Task<PagedList<GisDTO>> GetPaged(Params parameters)
        {
            var source = _db.Gises
                .Include(x => x.Names)
                .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result = await PagedList<Gis>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<GisDTO>>(result);

            return new PagedList<GisDTO>(entities, result.MetaData);
        }

        public async Task<GisDTO> IsUnique(GisDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.Gises.Include(x => x.Names)
                    .Where(x => x.Names.Any(n => dto.Name.ToLower().Equals(n.ToLower()))
                                || string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<Gis, GisDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.Gises.Include(x => x.Names)
                    .Where(x => (x.Names.Any(n => dto.Name.ToLower().Equals(n.ToLower()))
                                 || string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase))
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
            if (toUpdate == null) return null;
            var updated = _db.Gises.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<Gis, GisDTO>(updated.Entity);
            return result;
        }
    }
}