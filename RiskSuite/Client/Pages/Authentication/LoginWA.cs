using Microsoft.AspNetCore.Components;
using RiskSuite.Client.Services.IServices;
using RiskSuite.Shared.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RiskSuite.Client.Pages.Authentication
{
    public partial class LoginWA
    {
        public bool IsProcessing { get; set; } = false;
        public bool ShowAuthenticationErrors { get; set; }
        public string Errors { get; set; }

        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Parameter]
        public string User { get; set; }
        [Parameter]
        public string P { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var absoluteUri = new Uri(navigationManager.Uri);
            var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
            User = queryParam["user"];
            P = queryParam["p"];
            await Login();
        }

        public async Task Login()
        {
            ShowAuthenticationErrors = false;
            IsProcessing = true;
            AuthenticationDTO userForAuthentication = new AuthenticationDTO()
            {
                UserName = User,
                Password = P
            };
            var result = await authenticationService.Login(userForAuthentication);
            if (result.IsAuthSuccessful)
            {
                IsProcessing = false;
                navigationManager.NavigateTo("/");
            }
            else
            {
                IsProcessing = false;
                Errors = result.ErrorMessage;
                ShowAuthenticationErrors = true;
            }
        }
    }
}
