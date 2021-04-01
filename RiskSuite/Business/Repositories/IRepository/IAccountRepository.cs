using RiskSuite.Business;
using RiskSuite.Shared;
using RiskSuite.Shared.Authorization;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IAccountRepository
    {
        public Task<UserDTO> Create(UserDTO accountDTO);
        public Task<UserDTO> Update(UserDTO accountDTO);
        public Task<UserDTO> Get(string accountId);
        public Task<int> Delete(string accountId);
        public Task<IEnumerable<UserDTO>> GetAll();
        public Task<UserDTO> IsUnique(UserDTO accountDTO, string accountId = "");
        public Task<PagedList<UserDTO>> GetPaged(Params parameters);
    }
}
