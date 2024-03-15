using System;
using System.Threading;
using System.Threading.Tasks;
using RecipeBook.Core;
using RecipeBook.Core.Entities;
using RecipeBook.Core.Interfaces;
using RecipeBook.EF.Repositories;

namespace RecipeBook.EF
{
    public class RecipeUOW : IRecipeUOW
    {
        private readonly RecipeBookDBContext _context;

        public IBaseRepository<Recipe> Recipes { get; private set; }
        public IBaseRepository<RecipeIngredient> Ingredients { get; private set; }


        public RecipeUOW(RecipeBookDBContext context)
        {
            _context = context;
            Recipes = new BaseRepository<Recipe>(_context);
            Ingredients = new BaseRepository<RecipeIngredient>(_context);
        }

        public async Task<int> Complete(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
