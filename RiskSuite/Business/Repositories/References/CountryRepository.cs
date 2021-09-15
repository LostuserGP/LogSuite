using AutoMapper;
using Business.Repositories.IRepository.References;
using LogSuite.Business;
using LogSuite.Business.Repositories.References;
using LogSuite.DataAccess;
using LogSuite.DataAccess.References;
using LogSuite.Shared;
using LogSuite.Shared.Models.References;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Country toDb = _mapper.Map<CountryDTO, Country>(dto);
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
            if (fromDb != null)
            {
                var parent = await _db.GisCountries
                    .Where(x => x.CountryId == id)
                    .AnyAsync();
                if (parent)
                {
                    return -1;
                }
                else
                {
                    _db.Countries.Remove(fromDb);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;
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

        public async Task<IEnumerable<CountryDTO>> GetAll()
        {
            var entities = _db.Countries.Include(x => x.Names);
            IEnumerable<CountryDTO> dtos = _mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(entities);
            return dtos;
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
                var fromDb = await _db.Countries.Include(x => x.Names)
                    .Where(x => x.Names.Where(n => dto.Name.ToLower().Contains(n.Name.ToLower())).Any()
                        || x.Name.ToLower() == dto.Name.ToLower())
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<Country, CountryDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.Countries.Include(x => x.Names)
                    .Where(x => (x.Names.Where(n => dto.Name.ToLower().Contains(n.Name.ToLower())).Any()
                        || x.Name.ToLower() == dto.Name.ToLower())
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
            var updated = _db.Countries.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<Country, CountryDTO>(updated.Entity);
            return result;
        }
    }
}