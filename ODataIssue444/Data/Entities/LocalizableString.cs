﻿using System;
using Extenso.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ODataIssue444.Data.Entities
{
    public class LocalizableString : BaseEntity<Guid>
    {
        public string CultureCode { get; set; }

        public string TextKey { get; set; }

        public string TextValue { get; set; }
    }

    public class LocalizableStringMap : IEntityTypeConfiguration<LocalizableString>
    {
        public void Configure(EntityTypeBuilder<LocalizableString> builder)
        {
            builder.ToTable("Framework_LocalizableStrings");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.CultureCode).HasMaxLength(10).IsUnicode(false);
            builder.Property(m => m.TextKey).IsRequired().IsUnicode(true);
            builder.Property(m => m.TextValue).IsUnicode(true);
        }
    }
}