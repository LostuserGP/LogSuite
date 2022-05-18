using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.DataAccess;
using LogSuite.DataAccess.DailyReview;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.DailyReview;

public class GisCountryAddonTypeRepository : IGisCountryAddonTypeRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GisCountryAddonTypeRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<GisCountryAddonTypeDto> Create(GisCountryAddonTypeDto dto)
    {
        var toDb = _mapper.Map<GisCountryAddonTypeDto, GisCountryAddonType>(dto);
        var result = await _db.GisCountryAddonTypes.AddAsync(toDb);
        await _db.SaveChangesAsync();
        return _mapper.Map<GisCountryAddonType, GisCountryAddonTypeDto>(result.Entity);
    }

    public async Task<GisCountryAddonTypeDto> Update(GisCountryAddonTypeDto dto)
    {
        var fromDb = await _db.GisCountryAddonTypes
            .FirstOrDefaultAsync(x => x.Id == dto.Id);
        var toUpdate = _mapper.Map(dto, fromDb);
        if (toUpdate == null) return null;
        var updated = _db.GisCountryAddonTypes.Update(toUpdate);
        await _db.SaveChangesAsync();
        var result = _mapper.Map<GisCountryAddonType, GisCountryAddonTypeDto>(updated.Entity);
        return result;
    }

    public async Task<GisCountryAddonTypeDto> Get(int id)
    {
        var fromDb = await _db.GisCountryAddonTypes
            .FirstOrDefaultAsync(x => x.Id == id);
        var dto = _mapper.Map<GisCountryAddonType, GisCountryAddonTypeDto>(fromDb);
        return dto;
    }

    public async Task<int> Delete(int id)
    {
        var fromDb = await _db.GisCountryAddonTypes
            .FirstOrDefaultAsync(x => x.Id == id);
        if (fromDb == null) return 0;
        _db.GisCountryAddonTypes.Remove(fromDb);
        return await _db.SaveChangesAsync();
    }

    public Task<IEnumerable<GisCountryAddonTypeDto>> GetAll()
    {
        return null;
    }

    public async Task<GisCountryAddonTypeDto> IsUnique(GisCountryAddonTypeDto dto, int id = 0)
    {
        if (id == 0)
        {
            var fromDb = await _db.GisCountryAddonTypes
                .Where(x => x.StartDate == dto.StartDate && x.GisCountryAddonId == dto.GisCountryAddonId)
                .FirstOrDefaultAsync();
            var result = _mapper.Map<GisCountryAddonType, GisCountryAddonTypeDto>(fromDb);
            return result;
        }
        else
        {
            var fromDb = await _db.GisCountryAddonTypes
                .Where(x => x.StartDate == dto.StartDate && x.GisCountryAddonId == dto.GisCountryAddonId && x.Id != id)
                .FirstOrDefaultAsync();
            var result = _mapper.Map<GisCountryAddonType, GisCountryAddonTypeDto>(fromDb);
            return result;
        }
    }

    public Task<PagedList<GisCountryAddonTypeDto>> GetPaged(Params parameters)
    {
        return null;
    }

    public async Task<PagedList<GisCountryAddonTypeDto>> GetPagedByGisCountryAddonId(int gisCountryAddonId, Params parameters)
    {
        var source = _db.GisCountryAddonTypes
            .Where(x => x.GisCountryAddonId == gisCountryAddonId)
            .AsQueryable();
        // source = source.Search(parameters.Filter);
        //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
        var result =
            await PagedList<GisCountryAddonType>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
        var entities = _mapper.Map<List<GisCountryAddonTypeDto>>(result);
        return new PagedList<GisCountryAddonTypeDto>(entities, result.MetaData);
    }
}