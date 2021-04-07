using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System;
using RiskSuite.DataAccess;
using Microsoft.EntityFrameworkCore;
using RiskSuite.Server.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using RiskSuite.Server.Services.IServices;
using RiskSuite.Server.Services;
using Business.Repositories.IRepository;
using Business.Repositories;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Server.IISIntegration;
using Business.Repositories.IRepository.References;
using Business.Repositories.References;
using RiskSuite.Shared;
using RiskSuite.DataAccess.CredRisk;
using System.IdentityModel.Tokens.Jwt;

namespace RiskSuite.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention());

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 5;
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddClaimsPrincipalFactory<RolesClaimsPrincipalFactory>();

            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddRoles<IdentityRole>() // For Roles, see (client side) /RolesClaimsPrincipalFactory.cs
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            //    //.AddClaimsPrincipalFactory<RolesClaimsPrincipalFactory>(); // To add a custom claim for the user, see: /Models/ApplicationUser.cs

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
            //    {
            //        options.IdentityResources["openid"].UserClaims.Add("role"); // Roles
            //        options.ApiResources.Single().UserClaims.Add("role");
            //        options.IdentityResources["openid"].UserClaims.Add("custom_claim"); // Custom Claim
            //        options.ApiResources.Single().UserClaims.Add("custom_claim");
            //    });
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");


            var appSettingsSection = Configuration.GetSection("APISettings");
            services.Configure<APISettings>(appSettingsSection);

            var apiSettings = appSettingsSection.Get<APISettings>();
            var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);

            services.AddAuthentication(opt =>
            {
                //opt.DefaultAuthenticateScheme = IISDefaults.AuthenticationScheme;
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                //x.MapInboundClaims = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudience = apiSettings.ValidAudience,
                    ValidIssuer = apiSettings.ValidIssuer,
                    ClockSkew = TimeSpan.Zero
                };
            });
            


            //added for WA
            //services.AddAuthentication(IISDefaults.AuthenticationScheme);
            //services.AddHttpContextAccessor();
            //services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<ICounterpartyRepository, CounterpartyRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<ICommitteeLimitRepository, CommitteeLimitRepository>();
            services.AddScoped<ICommitteeStatusRepository, CommitteeStatusRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IFinancialStatementStandardRepository, FinancialStatementStandardRepository>();
            services.AddScoped<IGuaranteeApprovalDocTypeRepository, GuaranteeApprovalDocTypeRepository>();
            services.AddScoped<IGuaranteeTypeRepository, GuaranteeTypeRepository>();
            services.AddScoped<IRatingAgencyRepository, RatingAgencyRepository>();
            services.AddScoped<IRiskClassRepository, RiskClassRepository>();
            //services.AddScoped(typeof(IReferenceRepository<>), typeof(ReferenceRepository<>));
            services.AddScoped<IMailService, MailService>();

            services.AddCors(o => o.AddPolicy("RiskSuite", builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination");
            }));

            services.AddRouting(opt => opt.LowercaseUrls = true);

            services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RiskSuite_Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please bearer and then token in the field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RiskSuite_Api v1"));
            }


            app.UseCors("RiskSuite");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            dbInitializer.Initialize();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
