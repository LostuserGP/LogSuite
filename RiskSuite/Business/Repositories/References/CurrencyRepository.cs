using AutoMapper;
using Business.Repositories.IRepository.References;
using Microsoft.EntityFrameworkCore;
using LogSuite.Business;
using LogSuite.Business.Repositories;
using LogSuite.DataAccess;
using LogSuite.DataAccess.CredRisk;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogSuite.DataAccess.References;

namespace Business.Repositories.References
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
            Currency toDb = _mapper.Map<CurrencyDTO, Currency>(dto);
            var result = await _db.Currencies.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<Currency, CurrencyDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.Currencies.FindAsync(id);
            if (fromDb != null)
            {
                var parent = await _db.CurrencyRates
                    .Where(x => x.CurrencyFromId == id || x.CurrencyToId == id)
                    .AnyAsync();
                if (parent)
                {
                    return -1;
                }
                else
                {
                    _db.Currencies.Remove(fromDb);
                    return await _db.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<CurrencyDTO> Get(int id)
        {
            var fromDb = await _db.Currencies.FindAsync(id);
            var dto = _mapper.Map<Currency, CurrencyDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<CurrencyDTO>> GetAll()
        {
            try
            {
                var entities = _db.Currencies;
                IEnumerable<CurrencyDTO> dtos = _mapper.Map<IEnumerable<Currency>, IEnumerable<CurrencyDTO>>(entities);
                return dtos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CurrencyDTO> IsUnique(CurrencyDTO dto, int id = 0)
        {
            try
            {
                if (id == 0)
                {
                    var fromDb = await _db.Currencies
                        .FirstOrDefaultAsync(x => x.Name.ToLower() == dto.Name.ToLower());
                    var result = _mapper.Map<Currency, CurrencyDTO>(fromDb);
                    return result;
                }
                else
                {
                    var fromDb = await _db.Currencies
                        .FirstOrDefaultAsync(x => (x.Name.ToLower() == dto.Name.ToLower())
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

        public async Task<CurrencyDTO> Update(CurrencyDTO dto)
        {
            try
            {
                var fromDb = await _db.Currencies.FindAsync(dto.Id);
                var toUpdate = _mapper.Map(dto, fromDb);
                var updated = _db.Currencies.Update(toUpdate);
                await _db.SaveChangesAsync();
                var result = _mapper.Map<Currency, CurrencyDTO>(updated.Entity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}