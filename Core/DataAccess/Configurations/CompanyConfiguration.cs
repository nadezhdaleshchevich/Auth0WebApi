using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("companies");
            builder.HasKey(i => i.Id);
            builder.HasIndex(i => i.Id);
            builder.Property(i => i.Id).HasColumnType("int").HasColumnName("company_id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(i => i.Name).HasColumnType("varchar").HasColumnName("company_name").HasMaxLength(100).IsRequired();
        }
    }
}
