using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogSuite.Business.Repositories.IRepository;
using LogSuite.DataAccess;
using LogSuite.DataAccess.CredRisk;
using LogSuite.Shared;
using LogSuite.Shared.Models.CredRisk;
using Microsoft.EntityFrameworkCore;

namespace LogSuite.Business.Repositories.CredRisk
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
        public Task<CounterpartyDTO> Create(CounterpartyDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<CounterpartyDTO> Update(CounterpartyDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CounterpartyDTO> Get(int id)
        {
            var counterparty = await _db.Counterparties
                .Include(x => x.FinancialSector)
                .Include(x => x.Country)
                .Include(x => x.CountryRisk)
                .Include(x => x.CounterpartyPortfolios).ThenInclude(p => p.Portfolio)
                .Include(x => x.RatingDonor)
                .Include(x => x.CounterpartyGroup)
                .FirstOrDefaultAsync(x => x.Id == id);
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

        public Task<CounterpartyDTO> IsUnique(CounterpartyDTO dto, int id = 0)
        {
            throw new NotImplementedException();
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
            //source = source.Sort(parameters.OrderBy, parameters.OrderByAsc);
            var result = await PagedList<Counterparty>.ToPagedListAsync(source, parameters.Skip, parameters.Top);
            var counterparties = _mapper.Map<List<CounterpartyDTO>>(result);

            return new PagedList<CounterpartyDTO>(counterparties, result.MetaData);
        }

        public Task<CounterpartyDTO> IsUnique(string name, int counterpartyId = 0)
        {
            throw new NotImplementedException();
        }

        public Task<CounterpartyDTO> Update(int counterpartyId, CounterpartyDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
