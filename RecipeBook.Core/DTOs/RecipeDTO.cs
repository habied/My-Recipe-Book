using System.Text.Json.Serialization;

namespace RecipeBook.Core.DTOs
{
    public class RecipeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public List<RecipeIngredientDTO> Ingredients { get; set; }
    }
}
