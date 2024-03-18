using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBook.Core.Entities;

namespace RecipeBook.EF.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(r => r.Id); 

            builder.Property(r => r.Id).IsRequired().HasDefaultValueSql("NEWID()");
            builder.Property(r => r.UserName).IsRequired().HasMaxLength(100);
        }
    }
}
