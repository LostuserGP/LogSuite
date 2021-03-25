using Microsoft.AspNetCore.Components;
using RiskSuite.Client.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RiskSuite.Client.Pages.Authentication
{
    public partial class LoginWA
    {
        public string ReturnUrl { get; set; }

        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await Login();
        }

        public async Task Login()
        {
            var result = await authenticationService.LoginWA();
            if (result.IsAuthSuccessful)
            {
                var absoluteUri = new Uri(navigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = queryParam["returnUrl"];
                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/" + ReturnUrl);
                }
            }
            else
            {
            }
        }
    }
}
