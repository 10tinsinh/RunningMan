using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RunningManApi.DTO.Models;
using RunningManApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            services.AddControllers();
            //services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            services.Configure<AppSetting>(Configuration.GetSection("AppSettings"));
            var secretKey = Configuration["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            //services.AddAuthentication
            //    (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            //    {
            //        opt.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            //Auto get token
            //            ValidateIssuer = false,
            //            ValidateAudience = false,

            //            //Sign into token
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

            //            ClockSkew = TimeSpan.Zero

            //        };
            //        /*Check token invalid */
            //        opt.Events = new JwtBearerEvents()
            //        {
            //            OnAuthenticationFailed = context =>
            //            {
            //                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //                context.Response.ContentType = context.Request.Headers["Accept"].ToString();
            //                return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            //                {
            //                    StatusCode = (int)HttpStatusCode.Unauthorized,
            //                    Message = "Authentication token is invalid or may be expired."
            //                }));

                            
            //            }
            //        };
            //    });


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
            });
        }
    }
}
