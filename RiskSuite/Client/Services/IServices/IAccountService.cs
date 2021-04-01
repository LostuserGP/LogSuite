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
        Task<IEnumerable<UserDTO>> Getall();
        Task<UserDTO> Get(int accountId);
        Task<UserDTO> Create(UserDTO accountDTO);
        Task<PagingResponse<UserDTO>> Getall(Params parameters);
        Task<UserDTO> Update(UserDTO accountDTO);
    }
}
