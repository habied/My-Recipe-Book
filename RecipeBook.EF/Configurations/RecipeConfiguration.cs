using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBook.Core.Entities;

namespace RecipeBook.EF.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes");

            builder.HasKey(r => r.Id); 

            builder.Property(r => r.Id).IsRequired().HasDefaultValueSql("NEWID()");
            builder.Property(r => r.Name).IsRequired().HasMaxLength(255);
            builder.Property(r => r.Instructions).HasMaxLength(2000); 
            builder.Property(r => r.CreationDate).HasColumnType("DATETIME").IsRequired(); 
            builder.Property(r => r.CreatedById).IsRequired().HasMaxLength(50);
        }
    }
}
