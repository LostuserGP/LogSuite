using RiskSuite.Client.Helpers;
using RiskSuite.Shared;
using RiskSuite.Shared.Authorization;
using RiskSuite.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskSuite.Client.Services.IServices
{
    public interface IAccountService
    {
        Task<IEnumerable<UserDetailDTO>> Getall();
        Task<UserDetailDTO> Get(string accountId);
        Task<UserDetailDTO> Create(UserDetailDTO accountDTO);
        Task<PagingResponse<UserDetailDTO>> Getall(Params parameters);
        Task<UserDetailDTO> Update(UserDetailDTO accountDTO);
        Task<List<string>> GetRoles();
    }
}
