using AutoMapper;
using Business.Repositories.IRepository;
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

namespace LogSuite.Business.Repositories
{
    public class CounterpartyRepository : ICounterpartyRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CounterpartyRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public Task<CounterpartyDTO> Create(CounterpartyDTO counterpartyDTO)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int counterpartyId)
        {
            throw new NotImplementedException();
        }

        public async Task<CounterpartyDTO> Get(int counterpartyId)
        {
            var counterparty = await _db.Counterparties
                .Include(x => x.FinancialSector)
                .Include(x => x.Country)
                .Include(x => x.CountryRisk)
                .Include(x => x.CounterpartyPortfolios).ThenInclude(p => p.Portfolio)
                .Include(x => x.RatingDonor)
                .Include(x => x.CounterpartyGroup)
                .FirstOrDefaultAsync(x => x.Id == counterpartyId);
            var counterpartyDTO = _mapper.Map<Counterparty, CounterpartyDTO>(counterparty);
            return counterpartyDTO;
        }

        public async Task<IEnumerable<CounterpartyDTO>> GetAll()
        {
            try
            {
                var counterparties = _db.Counterparties
                    .Include(x => x.FinancialSector)
                    .Include(x => x.Country)
                    .Include(x => x.CountryRisk)
                    .Include(x => x.CounterpartyPortfolios).ThenInclude(p => p.Portfolio)
                    .Include(x => x.RatingDonor);
                IEnumerable<CounterpartyDTO> counterpartyDTOs = _mapper.Map<IEnumerable<Counterparty>, IEnumerable<CounterpartyDTO>>(counterparties);
                return counterpartyDTOs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PagedList<CounterpartyDTO>> GetPaged(Params parameters)
        {
            var source = _db.Counterparties
                    .Include(x => x.FinancialSector)
                    .Include(x => x.Country)
                    .Include(x => x.CountryRisk)
                    .Include(x => x.CounterpartyPortfolios).ThenInclude(p => p.Portfolio)
                    .Include(x => x.RatingDonor)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<Counterparty>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var counterparties = _mapper.Map<List<CounterpartyDTO>>(result);

            return new PagedList<CounterpartyDTO>(counterparties, result.MetaData);
        }

        public Task<CounterpartyDTO> IsUnique(string name, int counterpartyId = 0)
        {
            throw new NotImplementedException();
        }

        public Task<CounterpartyDTO> Update(int counterpartyId, CounterpartyDTO counterpartyDTO)
        {
            throw new NotImplementedException();
        }
    }
}
