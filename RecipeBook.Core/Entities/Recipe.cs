using System.Text.Json.Serialization;

namespace RecipeBook.Core.Entities
{
    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new HashSet<RecipeIngredient>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedById { get; set; }
        public virtual ICollection<RecipeIngredient> Ingredients { get; set; }

    }
}
