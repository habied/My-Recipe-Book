using System.Text.Json.Serialization;

using RecipeBook.Core.Enums;

namespace RecipeBook.Core.Entities
{
    public class RecipeIngredient
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public MeasuringUnit MeasuringUnit { get; set; }
        public string RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

    }
}
