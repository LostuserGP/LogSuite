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
    public interface IGisRepository
    {
        Task<GisDTO> Create(GisDTO dto);
        Task<GisDTO> Update(GisDTO dto);
        Task<GisDTO> Get(int id);
        Task<int> Delete(int id);
        Task<IEnumerable<GisDTO>> GetAll();
        Task<GisDTO> IsUnique(GisDTO dto, int id = 0);
        Task<PagedList<GisDTO>> GetPaged(Params parameters);
    }
}
