using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models.Operativka;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IGisCountryResourceRepository
    {
        Task<GisCountryResourceDTO> Create(GisCountryResourceDTO dto);
        Task<GisCountryResourceDTO> Update(GisCountryResourceDTO dto);
        Task<GisCountryResourceDTO> Get(int id);
        Task<int> Delete(int id);
        Task<GisCountryResourceDTO> IsUnique(GisCountryResourceDTO dto, int id = 0);
        Task<PagedList<GisCountryResourceDTO>> GetByGisCountryId(int gosCountryId, Params parameters);
    }
}
