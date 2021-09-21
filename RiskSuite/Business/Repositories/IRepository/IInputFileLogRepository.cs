using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using LogSuite.Shared.Models.DailyReview;
using LogSuite.Shared.Models.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IInputFileLogRepository
    {
        Task<InputFileLogDTO> Create(InputFileLogDTO dto);
        Task<InputFileLogDTO> Update(InputFileLogDTO dto);
        Task<InputFileLogDTO> Get(int id);
        Task<InputFileLogDTO> GetByFilename(string filename);
        Task<int> Delete(int id);
        Task<InputFileLogDTO> IsUnique(InputFileLogDTO dto, int id = 0);
    }
}
