using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(i => i.Id);
            builder.HasIndex(i => i.Id);
            builder.HasOne(i => i.Company).WithMany(i => i.Users).HasForeignKey(i => i.CompanyId);
            builder.Property(i => i.Id).HasColumnType("int").HasColumnName("user_id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(i => i.Auth0Id).HasColumnType("varchar(max)").HasColumnName("user_auth0_id").IsRequired(false);
            builder.Property(i => i.FirstName).HasColumnType("varchar").HasColumnName("user_first_name").HasMaxLength(100).IsRequired();
            builder.Property(i => i.LastName).HasColumnType("varchar").HasColumnName("_user_last_name").HasMaxLength(100).IsRequired();
            builder.Property(i => i.Email).HasColumnType("varchar(max)").HasColumnName("user_email").IsRequired(false);
            builder.Property(i => i.CompanyId).HasColumnType("int").HasColumnName("_user_company_id").IsRequired(false);
        }
    }
}
