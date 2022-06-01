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
            builder.HasKey(x => x.Id);
            builder.HasIndex(i => i.Id);
            builder.Property(i => i.Id).HasColumnName("user_id").IsRequired();
            builder.Property(i => i.UserName).HasColumnName("user_name").HasMaxLength(100).IsRequired();
        }
    }
}
