using RiskSuite.Shared.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiskSuite.Client.Services.IServices
{
    public interface IAuthenticationService
{
        Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegisteration);

        Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication);

        Task Logout();

        Task<AuthenticationResponseDTO> LoginWA();
    }
}
