using System;
using System.Collections.Generic;
using Autofac;
using Extenso.AspNetCore.OData;
using Extenso.Data.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ODataIssue444.Data;
using ODataIssue444.Data.Entities;
using ODataIssue444.Infrastructure;

namespace ODataIssue444
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews()
                .AddOData((options, serviceProvider) =>
                {
                    options.Select().Expand().Filter().OrderBy().SetMaxTop(null).Count();

                    var registrars = serviceProvider.GetRequiredService<IEnumerable<IODataRegistrar>>();
                    foreach (var registrar in registrars)
                    {
                        registrar.Register(options);
                    }
                });
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            SeedData(serviceProvider);
        }

        private void SeedData(IServiceProvider serviceProvider)
        {
            var personRepository = serviceProvider.GetRequiredService<IRepository<Person>>();
            if (personRepository.Count() == 0)
            {
                personRepository.Insert(new List<Person>
                {
                    new Person { FamilyName = "Jordan", GivenNames = "Michael", DateOfBirth = new DateTime(1963, 2, 17) },
                    new Person { FamilyName = "Johnson", GivenNames = "Dwayne", DateOfBirth = new DateTime(1972, 5, 2) },
                    new Person { FamilyName = "Froning", GivenNames = "Rich", DateOfBirth = new DateTime(1987, 7, 21) }
                });
            }

            var languageRepository = serviceProvider.GetRequiredService<IRepository<Language>>();
            if (languageRepository.Count() == 0)
            {
                languageRepository.Insert(new List<Language>
                {
                    new Language { Id = Guid.NewGuid(), CultureCode = "en-US", Name = "English (United States)", IsEnabled = true },
                    new Language { Id = Guid.NewGuid(), CultureCode = "jp-JP", Name = "Japanese (Japan)", IsEnabled = true }
                });
            }

            var localizableStringRepository = serviceProvider.GetRequiredService<IRepository<LocalizableString>>();
            if (localizableStringRepository.Count() == 0)
            {
                localizableStringRepository.Insert(new List<LocalizableString>
                {
                    new LocalizableString { Id = Guid.NewGuid(), CultureCode = null, TextKey = "Hello", TextValue = "Hello" }, // Invariant
                    new LocalizableString { Id = Guid.NewGuid(), CultureCode = "en-US", TextKey = "Hello", TextValue = "Hello" },
                    new LocalizableString { Id = Guid.NewGuid(), CultureCode = "jp-JP", TextKey = "Hello", TextValue = "こんにちは" }
                });
            }
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContextFactory>().As<IDbContextFactory>().SingleInstance();

            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<ODataRegistrar>().As<IODataRegistrar>().SingleInstance();
        }
    }
}