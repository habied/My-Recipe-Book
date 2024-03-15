using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBook.Core.Entities;

namespace RecipeBook.EF.Configurations
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.ToTable("RecipeIngredients");

            builder.HasKey(ri => ri.Id);

            builder.Property(ri => ri.Id).IsRequired().HasDefaultValueSql("NEWID()");
            builder.Property(ri => ri.Name).IsRequired().HasMaxLength(255);
            builder.Property(ri => ri.Quantity).HasColumnType("DECIMAL(8, 2)").IsRequired();
            builder.Property(ri => ri.MeasuringUnit)
                .IsRequired()
                .HasColumnType("INT")
                .HasConversion<int>();

            builder.Property(ri => ri.RecipeId).IsRequired();

            builder.HasOne(p => p.Recipe)
                .WithMany(pp => pp.Ingredients)
                .HasForeignKey(p => p.RecipeId);
        }
    }
}
