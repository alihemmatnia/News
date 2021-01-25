using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebMarkupMin.AspNetCore5;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using News.DataLayer.Context;
using News.Services.Services;
using News.Services.Repositories;
using Microsoft.AspNetCore.Identity;
using News.Utilities.IdentityPersian;

namespace News
{
    public class Startup
    {
        
        private readonly string connetcionstring = "data source=db.db";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
          
      
            services.AddWebMarkupMin(e => e.AllowMinificationInDevelopmentEnvironment = true)
                .AddHtmlMinification()
                .AddHttpCompression();
            services.AddDbContextPool<AppDbContext>(option =>
            {
                option.UseSqlite(connetcionstring);
            });

            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IPageGroupRepository, PageGroupRepository>();

            services.AddScoped<IEmailSend, EmailTokenSender>();
            services.AddIdentity<IdentityUser, IdentityRole>(option =>
            {
                option.Password.RequiredUniqueChars = 0;
                option.SignIn.RequireConfirmedEmail = true;
                option.Password.RequireNonAlphanumeric = false;
                option.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<IdentityPersian>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseRouting();
            app.UseWebMarkupMin();
	    app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
               
            });
        }
    }
}
