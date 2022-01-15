using System;
using Extenso.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ODataIssue444.Data.Entities
{
    public class Language : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public string CultureCode { get; set; }

        public bool IsRTL { get; set; }

        public bool IsEnabled { get; set; }

        public int SortOrder { get; set; }
    }

    public class LanguageMap : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Framework_Languages");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired().HasMaxLength(255).IsUnicode(true);
            builder.Property(m => m.CultureCode).IsRequired().HasMaxLength(10).IsUnicode(false);
            builder.Property(m => m.IsRTL).IsRequired();
            builder.Property(m => m.IsEnabled).IsRequired();
            builder.Property(m => m.SortOrder).IsRequired();
        }
    }
}