using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.Business.Repositories.IRepository.References;
using LogSuite.DataAccess;
using LogSuite.DataAccess.References;
using LogSuite.Shared;
using LogSuite.Shared.Models.References;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.References
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CountryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CountryDTO> Create(CountryDTO dto)
        {
            var toDb = _mapper.Map<CountryDTO, Country>(dto);
            var result = await _db.Countries.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<Country, CountryDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.Countries
                .Include(x => x.Names)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (fromDb == null) return 0;
            {
                var parent = await _db.GisCountries
                    .Where(x => x.CountryId == id)
                    .AnyAsync();
                if (parent)
                {
                    return -1;
                }

                _db.Countries.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
        }

        public async Task<CountryDTO> Get(int id)
        {
            var fromDb = await _db.Countries
                .Include(x => x.Names)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<Country, CountryDTO>(fromDb);
            return dto;
        }

        public Task<IEnumerable<CountryDTO>> GetAll()
        {
            var entities = _db.Countries.Include(x => x.Names);
            var dtos = _mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(entities);
            return Task.FromResult(dtos);
        }

        public async Task<PagedList<CountryDTO>> GetPaged(Params parameters)
        {
            var source = _db.Countries
                .Include(x => x.Names)
                .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<Country>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<CountryDTO>>(result);

            return new PagedList<CountryDTO>(entities, result.MetaData);
        }

        public async Task<CountryDTO> IsUnique(CountryDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.Countries
                    .Where(x => string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<Country, CountryDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.Countries
                    .Where(x => string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase)
                                && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<Country, CountryDTO>(fromDb);
                return result;
            }
        }

        public async Task<CountryDTO> Update(CountryDTO dto)
        {
            var fromDb = await _db.Countries
                .FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.Countries.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<Country, CountryDTO>(updated.Entity);
            return result;

        }
    }
}