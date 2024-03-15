using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Entities;
using RecipeBook.EF.Configurations;

namespace RecipeBook.EF
{
    public class RecipeBookDBContext: IdentityDbContext<IdentityUser>
    {
        public RecipeBookDBContext(DbContextOptions<RecipeBookDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RecipeConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeIngredientConfiguration());
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    }
}
