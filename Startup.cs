using Microsoft.AspNetCore.Authorization;  
using System.IdentityModel.Tokens.Jwt;  
using System.Security.Claims;  
using Microsoft.IdentityModel.Tokens;  
using System.Text;   
using Microsoft.AspNetCore.Authentication.JwtBearer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            // specifying the autheticaiotn way
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  
            .AddJwtBearer(options =>  
            {  
            options.TokenValidationParameters = new TokenValidationParameters  
            {  
                ValidateIssuer = true,  
                ValidateAudience = true,  
                ValidateLifetime = true,  
                ValidateIssuerSigningKey = true,  
                ValidIssuer = Configuration["Jwt:Issuer"],  
                ValidAudience = Configuration["Jwt:Issuer"],  
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))  
            };  
            });  
             services.AddSession(options =>
            {
            options.Cookie.IsEssential = true;
            
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseAuthentication();  

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
             app.UseSession();
            app.Use(async (context, next) =>
     {
        var JWToken = context.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(JWToken))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            }
            await next();
        });
         app.UseAuthentication();  
            

            app.UseMvc(
                routes =>
                {
                    routes.MapRoute("DefaultRoute", "{controller}/{action}");
                    routes.MapRoute(name: "Default", template: "{controller}/{action}", defaults: new { controller = "Login", action = "Login" });
                }
                );
            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //         name: "default",
            //         template: "{controller=Home}/{action=Index}/{id?}");
            // });
        }
    }
}
