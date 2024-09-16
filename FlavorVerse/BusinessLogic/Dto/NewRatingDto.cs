namespace FlavorVerse.BusinessLogic.Dto
{
    public class NewRatingDto
    {
        public int RatingValue { get; set; }
        public string Comment { get; set; } = string.Empty;
        public Guid RecipeId { get; set; }
    }
}