//using Extenso.Data.Entity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace ODataIssue444.Data.Entities
//{
//    public class LocalizableProperty : BaseEntity<int>
//    {
//        public string CultureCode { get; set; }

//        public string EntityType { get; set; }

//        public string EntityId { get; set; }

//        public string Property { get; set; }

//        public string Value { get; set; }
//    }

//    public class LocalizablePropertyMap : IEntityTypeConfiguration<LocalizableProperty>
//    {
//        public void Configure(EntityTypeBuilder<LocalizableProperty> builder)
//        {
//            builder.ToTable("Framework_LocalizableProperties");
//            builder.HasKey(m => m.Id);
//            builder.Property(m => m.CultureCode).HasMaxLength(10).IsUnicode(false);
//            builder.Property(x => x.EntityType).IsRequired().HasMaxLength(512).IsUnicode(false);
//            builder.Property(x => x.EntityId).IsRequired().HasMaxLength(50).IsUnicode(false);
//            builder.Property(m => m.Property).IsRequired().HasMaxLength(128).IsUnicode(false);
//            builder.Property(m => m.Value).IsUnicode(true);
//        }
//    }
//}