using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISSystem.DbContext;
using Microsoft.EntityFrameworkCore;
using ISSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using AutoMapper;
using InterviewSchedulingSystem.Services;
using ISSystem.DbContext.Repositories;
using ISSystem.DbContext.Interfaces;
using System.Globalization;
using Repositories;
using InterviewSchedulingSystem.Resources;
using TelegramBot;
using Hangfire;
using Hangfire.PostgreSql;

namespace InterviewSchedulingSystem
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connection));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddErrorDescriber<RussianIdentityErrorDescriber>(); ;
            services.AddControllersWithViews();



            services.AddHangfire(x => x.UsePostgreSqlStorage(connection));
            services.AddHangfireServer();


            services.AddTransient(typeof(IRepository<>), typeof(RepositoryBase<>));


            services.AddTransient<RepositoriesUnitOfWork>();

            //services.AddTransient<CalendarRepository>();
            //services.AddTransient<ScheduleRepository>();
            //services.AddTransient<CandidateRepository>();
            //services.AddTransient<VacancyRepository>();
            //services.AddTransient<RecordingRepository>();

            services.AddTransient<ScheduleService>();
            services.AddTransient<TemplateService>();
            //services.AddTransient<CandidateService>();
            services.AddTransient<RecordingService>();
            services.AddTransient<CalendarService>();
            services.AddTransient<VacancyService>();
            

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("ru-RU");
            });
            var cultureInfo = new CultureInfo("ru-RU");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddAutoMapper(typeof(Startup));
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options => //CookieAuthenticationOptions
            //    {
            //        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/");
            //    });
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

            app.UseRouting();

            app.UseAuthentication();    // подключение аутентификации
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "auto_link_area",
                    areaName: "autolink",
                    pattern: "autolink/{action=Invite}/{id?}");
                endpoints.MapAreaControllerRoute(
                    name: "admin_area",
                    areaName: "admin",
                    pattern: "admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            InterviewSchedulingSystem.Hangfire.AutoLink.connectString 
                = Configuration.GetConnectionString("DefaultConnection");

            StartBot.Run(Configuration.GetConnectionString("DefaultConnection"));
            //TelegramBot.CreateTelegramBot(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
