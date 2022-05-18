using System.Collections.Generic;
using System.Threading.Tasks;
using LogSuite.Shared;
using LogSuite.Shared.Authorization;

namespace LogSuite.Business.Repositories.IRepository
{
    public interface IAccountRepository
    {
        public Task<UserDTO> Create(UserDTO dto);
        public Task<UserDTO> Update(UserDTO dto);
        public Task<UserDTO> Get(string accountId);
        public Task<int> Delete(string accountId);
        public Task<IEnumerable<UserDTO>> GetAll();
        public Task<UserDTO> IsUnique(UserDTO accountDto, string accountId = "");
        public Task<PagedList<UserDTO>> GetPaged(Params parameters);
    }
}
