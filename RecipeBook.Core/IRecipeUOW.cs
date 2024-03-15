using System;
using System.Threading.Tasks;
using RecipeBook.Core.Entities;

namespace RecipeBook.Core.Interfaces
{
    public interface IRecipeUOW : IDisposable
    {
        IBaseRepository<Recipe> Recipes { get; }

        IBaseRepository<RecipeIngredient> Ingredients { get; }
        Task<int> Complete(CancellationToken token);
    }
}
