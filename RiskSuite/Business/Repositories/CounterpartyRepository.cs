using AutoMapper;
using Business.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using RiskSuite.Business;
using RiskSuite.DataAccess;
using RiskSuite.DataAccess.CredRisk;
using RiskSuite.Shared;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
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

        public Task<CounterpartyDTO> Get(int counterpartyId)
        {
            throw new NotImplementedException();
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
            if (!string.IsNullOrEmpty(parameters.Order))
            {
                if (parameters.OrderAsc)
                {
                    switch (parameters.Order)
                    {
                        case "Id":
                            source = source.OrderBy(s => s.Id);
                            break;
                        case "Name":
                            source = source.OrderBy(s => s.Name);
                            break;
                        case "ShortName":
                            source = source.OrderBy(s => s.ShortName);
                            break;
                        case "Sector":
                            source = source.OrderBy(s => s.FinancialSector.Name);
                            break;
                        case "CountryRisk":
                            source = source.OrderBy(s => s.CountryRisk.Name);
                            break;
                        case "CountryDom":
                            source = source.OrderBy(s => s.Country.Name);
                            break;
                        case "Donor":
                            source = source.OrderBy(s => s.RatingDonor.Name);
                            break;
                        case "DUNS":
                            source = source.OrderBy(s => s.Duns);
                            break;
                        case "Causes":
                            source = source.OrderBy(s => s.Causes);
                            break;
                        case "StartDate":
                            source = source.OrderBy(s => s.DateStart);
                            break;
                    }
                }
                else
                {
                    switch (parameters.Order)
                    {
                        case "Id":
                            source = source.OrderByDescending(s => s.Id);
                            break;
                        case "Name":
                            source = source.OrderByDescending(s => s.Name);
                            break;
                        case "ShortName":
                            source = source.OrderByDescending(s => s.ShortName);
                            break;
                        case "Sector":
                            source = source.OrderByDescending(s => s.FinancialSector.Name);
                            break;
                        case "CountryRisk":
                            source = source.OrderByDescending(s => s.CountryRisk.Name);
                            break;
                        case "CountryDom":
                            source = source.OrderByDescending(s => s.Country.Name);
                            break;
                        case "Donor":
                            source = source.OrderByDescending(s => s.RatingDonor.Name);
                            break;
                        case "DUNS":
                            source = source.OrderByDescending(s => s.Duns);
                            break;
                        case "Causes":
                            source = source.OrderByDescending(s => s.Causes);
                            break;
                        case "StartDate":
                            source = source.OrderByDescending(s => s.DateStart);
                            break;
                    }
                }
            }
            else
            {
                source = source.OrderBy(s => s.Name);
            }
            if (!string.IsNullOrEmpty(parameters.Filter))
            {
                var f = parameters.Filter.ToLower();
                source = source.Where(s => s.Name.ToLower().Contains(f)
                        || s.ShortName.ToLower().Contains(f)
                        || s.FinancialSector.Name.ToLower().Contains(f)
                        || s.Country.Name.ToLower().Contains(f)
                        || s.CountryRisk.Name.ToLower().Contains(f)
                        || s.RatingDonor.Name.ToLower().Contains(f)
                        || s.Duns.ToLower().Contains(f)
                        || s.Causes.ToLower().Contains(f)
                        );
            }
            var result = await PagedList<Counterparty>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            //var counterpartyList = result.ToList();
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
