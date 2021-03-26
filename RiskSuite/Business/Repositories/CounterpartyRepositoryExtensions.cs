using RiskSuite.DataAccess.CredRisk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskSuite.Business.Repositories
{
    public static class CounterpartyRepositoryExtensions
    {
        public static IQueryable<Counterparty> Search(this IQueryable<Counterparty> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result =  source.Where(s => s.Name.ToLower().Contains(f)
                        || s.ShortName.ToLower().Contains(f)
                        || s.FinancialSector.Name.ToLower().Contains(f)
                        || s.Country.Name.ToLower().Contains(f)
                        || s.CountryRisk.Name.ToLower().Contains(f)
                        || s.RatingDonor.Name.ToLower().Contains(f)
                        || s.Duns.ToLower().Contains(f)
                        || s.Causes.ToLower().Contains(f)
                        );
            return result;
        }

        public static IQueryable<Counterparty> Sort(this IQueryable<Counterparty> source, string columnName, bool ascendind)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                source = source.OrderBy(s => s.Name);
            } 
            else
            {
                if (ascendind)
                {
                    switch (columnName)
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
                            source = source.OrderBy(s => s.FinancialSector.NameEn);
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
                        default:
                            source = source.OrderBy(s => s.Name);
                            break;
                    }
                }
                else
                {
                    switch (columnName)
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
                        default:
                            source = source.OrderByDescending(s => s.Name);
                            break;

                    }
                }
            }
            return source;
        }
    }
}
