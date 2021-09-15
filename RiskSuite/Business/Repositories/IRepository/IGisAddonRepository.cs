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
    public interface IGisAddonRepository
    {
        Task<GisAddonDTO> Create(GisAddonDTO dto);
        Task<GisAddonDTO> Update(GisAddonDTO dto);
        Task<GisAddonDTO> Get(int id);
        Task<int> Delete(int id);
        Task<IEnumerable<GisAddonDTO>> GetAll();
        Task<GisAddonDTO> IsUnique(GisAddonDTO dto, int id = 0);
        Task<PagedList<GisAddonDTO>> GetPaged(Params parameters);
        Task<IEnumerable<GisAddonDTO>> GetAllByGisId(int gisId);
        Task<PagedList<GisAddonDTO>> GetPagedByGisId(int gisId, Params parameters);
    }
}
