using LocalizationUsingAcceptLangauage.WebAPI.Data;
using LocalizationUsingAcceptLangauage.WebAPI.Extentions;
using LocalizationUsingAcceptLangauage.WebAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationUsingAcceptLangauage.WebAPI
{
    public class Startup
    {
        public const string DatabaseFileName = "database.db";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStringLocalizer, CustomStringLocalizer>();
            services.AddScoped<IMoviesService, MoviesService>();
            services.AddDbContext<DatabaseContext>(options => options.UseLazyLoadingProxies().UseSqlite($"Data Source={DatabaseFileName}"));
            services.AddLocalization(option => option.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("fr"),
                        new CultureInfo("ru"),
                        new CultureInfo("it")
                    };
                    options.DefaultRequestCulture = new RequestCulture(culture: "en-us", uiCulture: "en-us");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    //options.RequestCultureProviders = new[] { new Extentions.RouteDataRequestCultureProvider { IndexOfCulture = 1, IndexOfUICulture = 1 } };
                    //options.RequestCultureProviders.Insert(0, new Extentions.RouteDataRequestCultureProvider());
                });
            //services.Configure<RouteOptions>(
            //    options =>
            //    {
            //        options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
            //    });
            services.AddMvc()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<SetLanguageMiddleware>();

            var localization = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localization.Value);
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
