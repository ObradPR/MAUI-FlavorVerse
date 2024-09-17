using Microsoft.AspNetCore.Http;

namespace FlavorVerse.BusinessLogic.Dto
{
    public class AddRecipeDto
    {
        public string Title { get; set; } = string.Empty;

        public IFormFile Image { get; set; }

        public string Description { get; set; } = string.Empty;

        public string CookingTime { get; set; } = string.Empty;

        public int Servings { get; set; }

        public string Instructions { get; set; } = string.Empty;

        public int MealTypeId { get; set; }

        public int? Protein { get; set; }

        public int? Carbohydrates { get; set; }

        public int? Fat { get; set; }

        public int? Fiber { get; set; }

        public bool? GlutenFree { get; set; }

        public bool? DairyFree { get; set; }

        public bool? Vegetarian { get; set; }

        public bool? Vegan { get; set; }

        public int DifficultyCookingId { get; set; }

        public int MainCategoryId { get; set; }

        public int MainCuisineId { get; set; }
    }
}