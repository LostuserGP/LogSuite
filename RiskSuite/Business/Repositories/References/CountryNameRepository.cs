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

namespace Business.Repositories.References
{
    public class CountryNameRepository : ICountryNameRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CountryNameRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<CountryNameDTO> Create(CountryNameDTO dto)
        {
            CountryName toDb = _mapper.Map<CountryNameDTO, CountryName>(dto);
            var result = await _db.CountryNames.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<CountryName, CountryNameDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.CountryNames
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (fromDb != null)
            {
                _db.CountryNames.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<CountryNameDTO> Get(int id)
        {
            var fromDb = await _db.CountryNames.FindAsync(id);
            var dto = _mapper.Map<CountryName, CountryNameDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<CountryNameDTO>> GetAll()
        {
            var entities = _db.CountryNames;
            IEnumerable<CountryNameDTO> dtos = _mapper.Map<IEnumerable<CountryName>, IEnumerable<CountryNameDTO>>(entities);
            return dtos;
        }

        public async Task<IEnumerable<CountryNameDTO>> GetByCountryId(int id)
        {
            var entities = _db.CountryNames.Where(x => x.CountryId == id);
            IEnumerable<CountryNameDTO> dtos = _mapper.Map<IEnumerable<CountryName>, IEnumerable<CountryNameDTO>>(entities);
            return dtos;
        }

        public async Task<PagedList<CountryNameDTO>> GetPagedByCountryId(int countryId, Params parameters)
        {
            var source = _db.CountryNames
                    .Where(x => x.CountryId == countryId)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<CountryName>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<CountryNameDTO>>(result);

            return new PagedList<CountryNameDTO>(entities, result.MetaData);
        }

        public async Task<CountryNameDTO> IsUnique(CountryNameDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.CountryNames
                    .Where(x => x.Name.ToLower().Equals(dto.Name.ToLower()))
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<CountryName, CountryNameDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.CountryNames
                    .Where(x => x.Name.ToLower().Equals(dto.Name.ToLower()))
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<CountryName, CountryNameDTO>(fromDb);
                return result;
            }
        }

        public async Task<CountryNameDTO> Update(CountryNameDTO dto)
        {
            var fromDb = await _db.CountryNames
                .FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            var updated = _db.CountryNames.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<CountryName, CountryNameDTO>(updated.Entity);
            return result;
        }
    }
}