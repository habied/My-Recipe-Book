using System.Text.Json.Serialization; 

using RecipeBook.Core.Enums;

namespace RecipeBook.Core.DTOs
{
    public class RecipeIngredientDTO
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public MeasuringUnit MeasuringUnit { get; set; }
    }
}
