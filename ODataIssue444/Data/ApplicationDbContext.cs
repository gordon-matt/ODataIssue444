using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ODataIssue444.Data.Entities;

namespace ODataIssue444.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new LanguageMap());
            builder.ApplyConfiguration(new LocalizableStringMap());
            builder.ApplyConfiguration(new PersonMap());

            // Doesn't seem to work...
            //builder.Entity<Person>().HasData(
            //    new Person { Id = 1, FamilyName = "Jordan", GivenNames = "Michael", DateOfBirth = new DateTime(1963, 2, 17) },
            //    new Person { Id = 2, FamilyName = "Johnson", GivenNames = "Dwayne", DateOfBirth = new DateTime(1972, 5, 2) },
            //    new Person { Id = 3, FamilyName = "Froning", GivenNames = "Rich", DateOfBirth = new DateTime(1987, 7, 21) }
            //);

            //builder.Entity<Language>().HasData(
            //    new Language { Id = Guid.NewGuid(), CultureCode = "en-US", Name = "English (United States)", IsEnabled = true },
            //    new Language { Id = Guid.NewGuid(), CultureCode = "jp-JP", Name = "Japanese (Japan)", IsEnabled = true }
            //);

            //builder.Entity<LocalizableString>().HasData(
            //    new LocalizableString { Id = Guid.NewGuid(), CultureCode = "en-US", TextKey = "Hello", TextValue = "Hello" },
            //    new LocalizableString { Id = Guid.NewGuid(), CultureCode = "jp-JP", TextKey = "Hello", TextValue = "こんにちは" }
            //);
        }
    }
}