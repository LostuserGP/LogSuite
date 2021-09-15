using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.Operativka;
using LogSuite.Shared.Models.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IGisAddonNameRepository
    {
        Task<GisAddonNameDTO> Create(GisAddonNameDTO dto);
        Task<GisAddonNameDTO> Update(GisAddonNameDTO dto);
        Task<GisAddonNameDTO> Get(int id);
        Task<int> Delete(int id);
        Task<IEnumerable<GisAddonNameDTO>> GetAll();
        Task<GisAddonNameDTO> IsUnique(GisAddonNameDTO dto, int id = 0);
        Task<PagedList<GisAddonNameDTO>> GetPaged(Params parameters);
        Task<IEnumerable<GisAddonNameDTO>> GetAllByGisAddonId(int gisAddonId);
        Task<PagedList<GisAddonNameDTO>> GetPagedByGisAddonId(int gisAddonId, Params parameters);
    }
}
