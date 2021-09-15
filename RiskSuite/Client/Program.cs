using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using LogSuite.Client.Helpers;
using LogSuite.Client.Serices;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication.Negotiate;

namespace LogSuite.Client
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

            builder.Services.AddSweetAlert2(options => { options.Theme = SweetAlertTheme.Dark; });
            builder.Services.AddScoped<ToastService>();

            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ICounterpartyService, CounterpartyService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IReferenceService, ReferenceService>();
            builder.Services.AddScoped<IRatingAgencyService, RatingAgencyService>();
            builder.Services.AddScoped<IRatingService, RatingService>();
            builder.Services.AddScoped<IRatingInternalService, RatingInternalService>();
            builder.Services.AddScoped<IRatingExternalService, RatingExternalService>();
            builder.Services.AddScoped<IFinancialStatementService, FinancialStatementService>();
            builder.Services.AddScoped<IFinancialStatementStandardService, FinancialStatementStandardService>();
            builder.Services.AddScoped<IRiskClassService, RiskClassService>();
            builder.Services.AddScoped<ICommitteeService, CommitteeService>();
            builder.Services.AddScoped<ICommitteeLimitService, CommitteeLimitService>();
            builder.Services.AddScoped<ICommitteeStatusService, CommitteeStatusService>();

            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<ICountryNameService, CountryNameService>();
            builder.Services.AddScoped<IGisService, GisService>();
            builder.Services.AddScoped<IGisNameService, GisNameService>();
            builder.Services.AddScoped<IGisInputNameService, GisInputNameService>();
            builder.Services.AddScoped<IGisOutputNameService, GisOutputNameService>();
            builder.Services.AddScoped<IGisCountryService, GisCountryService>();
            builder.Services.AddScoped<IGisAddonService, GisAddonService>();
            builder.Services.AddScoped<IGisAddonNameService, GisAddonNameService>();
            builder.Services.AddScoped<IFileTypeSettingService, FileTypeSettingService>();
            builder.Services.AddScoped<IReviewValueService, ReviewValueService>();

            //builder.Services.AddScoped<IReferenceService, CommitteeStatusService>();

            await builder.Build().RunAsync();
        }
    }
}
