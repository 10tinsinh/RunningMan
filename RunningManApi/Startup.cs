using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RunningManApi.DTO.Models;
using RunningManApi.Helpers;
using RunningManApi.Repository.Entites;
using RunningManApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningManApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration )
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson();
            services.AddScoped<IRoundRepository, RoundRepository>();
            services.AddScoped<IGamePlayRepository, GamePlayRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IGameTypeRepository, GameTypeRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddSingleton<IPermissionRepository, PermissionRepository>();


            services.AddOData();

            services.Configure<AppSetting>(Configuration.GetSection("AppSettings"));
            var secretKey = Configuration["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            services.AddAuthentication
                (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Auto get token
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        //Sign into token
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                        ClockSkew = TimeSpan.Zero

                    };
                });
            /*Authorization filter */
            services.AddAuthorization(configure =>
            {
                #region Filter User
                configure.AddPolicy(PolicyCode.ADMIN, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.AddRequirements(new RoleRequirement(PolicyCode.ADMIN));
                    policyBuilder.AddRequirements(new PermissionRequirement(PermissionCode.RUNNING_MAN_USER_VIEW));
                });
                configure.AddPolicy(PolicyCode.VIEW_USER, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.AddRequirements(new PermissionRequirement(PermissionCode.RUNNING_MAN_USER_VIEW));

                });
                configure.AddPolicy(PolicyCode.DELETE_USER, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.AddRequirements(new RoleRequirement(PolicyCode.ADMIN));
                    policyBuilder.AddRequirements(new PermissionRequirement(PermissionCode.RUNNING_MAN_USER_DELETE));

                });
                configure.AddPolicy(PolicyCode.UPDATE_USER, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();

                    policyBuilder.AddRequirements(new PermissionRequirement(PermissionCode.RUNNING_MAN_USER_UPDATE));

                });
                #endregion

                #region Filter Team
                configure.AddPolicy(PolicyCode.CREATE_TEAM, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.AddRequirements(new PermissionRequirement(PermissionCode.RUNNING_MAN_TEAM_CREATE));
                });
                configure.AddPolicy(PolicyCode.TEAM_LEAD, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.AddRequirements(new PermissionRequirement(PermissionCode.RUNNING_MAN_TEAM_LEADER));
                });
                configure.AddPolicy(PolicyCode.TEAM_MEMBER, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.AddRequirements(new PermissionRequirement(PermissionCode.RUNNING_MAN_TEAM_MEMBER));
                });
                configure.AddPolicy(PolicyCode.JOIN_TEAM, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.AddRequirements(new PermissionRequirement(PermissionCode.RUNNING_MAN_TEAM_JOIN));
                });
                
                #endregion

            });

            services.AddSingleton<IRoleRepository, RoleRepository>();
            services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
            services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RunningManApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RunningManApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.EnableDependencyInjection();
                endpoints.Select().Count().Filter().OrderBy().MaxTop(100).SkipToken().Expand();
            });
        }
    }
}
