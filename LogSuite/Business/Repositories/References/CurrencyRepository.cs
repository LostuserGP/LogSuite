using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.DataAccess;
using LogSuite.DataAccess.References;
using LogSuite.Shared;
using LogSuite.Shared.Models.CredRisk;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.References
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CurrencyRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CurrencyDTO> Create(CurrencyDTO dto)
        {
            var toDb = _mapper.Map<CurrencyDTO, Currency>(dto);
            var result = await _db.Currencies.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<Currency, CurrencyDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.Currencies.FindAsync(id);
            if (fromDb == null) return 0;
            var parent = await _db.CurrencyRates
                .Where(x => x.CurrencyFromId == id || x.CurrencyToId == id)
                .AnyAsync();
            if (parent)
            {
                return -1;
            }

            _db.Currencies.Remove(fromDb);
            return await _db.SaveChangesAsync();
        }

        public async Task<CurrencyDTO> Get(int id)
        {
            var fromDb = await _db.Currencies.FindAsync(id);
            var dto = _mapper.Map<Currency, CurrencyDTO>(fromDb);
            return dto;
        }

        public Task<IEnumerable<CurrencyDTO>> GetAll()
        {
            try
            {
                var entities = _db.Currencies;
                var dtos = _mapper.Map<IEnumerable<Currency>, IEnumerable<CurrencyDTO>>(entities);
                return Task.FromResult(dtos);
            }
            catch (Exception ex)
            {
                return Task.FromResult<IEnumerable<CurrencyDTO>>(null);
            }
        }

        public async Task<CurrencyDTO> IsUnique(CurrencyDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.Currencies
                        .FirstOrDefaultAsync(x =>
                            string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase));
                    var result = _mapper.Map<Currency, CurrencyDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.Currencies
                        .FirstOrDefaultAsync(x =>
                            string.Equals(x.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase)
                            && x.Id != id);
                    var result = _mapper.Map<Currency, CurrencyDTO>(fromDb);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PagedList<CurrencyDTO>> GetPaged(Params parameters)
        {
            var source = _db.Currencies
                .AsQueryable();
            source = source.Search(parameters.Filter);
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result = await PagedList<Currency>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var entities = _mapper.Map<List<CurrencyDTO>>(result);

            return new PagedList<CurrencyDTO>(entities, result.MetaData);
        }

        public async Task<CurrencyDTO> Update(CurrencyDTO dto)
        {
            var fromDb = await _db.Currencies.FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            if (toUpdate == null) return null;
            var updated = _db.Currencies.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<Currency, CurrencyDTO>(updated.Entity);
            return result;
        }
    }
}