using Blazored.LocalStorage;
using LogSuite.Client.Helpers;
using LogSuite.Client.Services;
using LogSuite.Client.Services.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Radzen;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using LogSuite.Client.Services.DailyReview;
using LogSuite.Client.Services.DailyReview.References;
using LogSuite.Client.Services.References;
using LogSuite.Client.Shared;

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
            builder.Services.AddScoped<IGisService, GisService>();
            builder.Services.AddScoped<IGisCountryService, GisCountryService>();
            builder.Services.AddScoped<IGisAddonService, GisAddonService>();
            builder.Services.AddScoped<IFileTypeSettingService, FileTypeSettingService>();
            builder.Services.AddScoped<IReviewValueService, ReviewValueService>();
            builder.Services.AddScoped<IGisCountryValueService, GisCountryValueService>();
            builder.Services.AddScoped<IGisAddonValueService, GisAddonValueService>();
            builder.Services.AddScoped<IGisInputValueService, GisInputValueService>();
            builder.Services.AddScoped<IGisOutputValueService, GisOutputValueService>();
            builder.Services.AddScoped<IGisCountryResourceService, GisCountryResourceService>();
            builder.Services.AddScoped<IInputFileLogService, InputFileLogService>();
            
            builder.Services.AddScoped<IGisCountryAddonService, GisCountryAddonService>();
            builder.Services.AddScoped<IGisCountryAddonTypeService, GisCountryAddonTypeService>();
            builder.Services.AddScoped<IGisCountryAddonValueService, GisCountryAddonValueService>();
            builder.Services.AddScoped<IGisCountryAddonTypeService, GisCountryAddonTypeService>();

            //builder.Services.AddScoped<IReferenceService, CommitteeStatusService>();
            builder.Services.AddScoped<ThemeState>();
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            await builder.Build().RunAsync();
        }
    }
}
