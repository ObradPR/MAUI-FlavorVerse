namespace FlavorVerse.BusinessLogic.Dto
{
    public class RecipeDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string CookingTime { get; set; } = string.Empty;

        public int Servings { get; set; }

        public bool Featured { get; set; }

        public string Instructions { get; set; } = string.Empty;

        public UserDto User { get; set; }

        public DifficultyCookingDto DifficultyCooking { get; set; }

        public DietaryInfoDto DietaryInfo { get; set; }

        public MealTypeDto MealType { get; set; }

        public NutritionDto Nutrition { get; set; }

        public List<RatingDto> Ratings { get; set; }

        public List<CuisineDto> Cuisines { get; set; }

        public List<IngredientDto> Ingredients { get; set; }

        public List<CategoryDto> Categories { get; set; }

        // CSV

        public string CuisinesCSV => string.Join(", ", Cuisines.Select(c => c.Name));

        public string CategoriesCSV => string.Join(", ", Categories.Select(_ => _.Name));

        // Formated

        public string FormattedIngredients => string.Join("\n", Ingredients.Select(i => $"{i.Name} - {i.Quantity}"));
    }
}