using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiskSuite.Client.Helpers;
using RiskSuite.Client.Services;
using RiskSuite.Client.Services.IServices;
using RiskSuite.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication.Negotiate;

namespace RiskSuite.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });

            //builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

            builder.Services.AddBlazoredLocalStorage();
            //builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped(typeof(AccountClaimsPrincipalFactory<RemoteUserAccount>), typeof(RolesAccountClaimsPrincipalFactory));
            //builder.Services.AddAuthorizationCore(opt =>
            //{
            //    opt.AddPolicy("RequireAdmin", policy => policy.RequireRole("[\"Admin\",\"RiskManager\"]"));
            //    opt.AddPolicy("RequireAdminTest", policy => policy.RequireRole("Admin"));
            //});

            builder.Services.AddApiAuthorization().AddAccountClaimsPrincipalFactory<RolesAccountClaimsPrincipalFactory>();

            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ICounterpartyService, CounterpartyService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IReferenceService, ReferenceService>();
            //builder.Services.AddScoped<IReferenceService, CommitteeStatusService>();

            await builder.Build().RunAsync();
        }
    }
}
