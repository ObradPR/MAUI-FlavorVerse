namespace FlavorVerse.BusinessLogic.Dto
{
    public class RatingDto
    {
        public Guid Id { get; set; }

        public int RatingValue { get; set; }

        public string Comment { get; set; } = string.Empty;

        public RatingUserDto User { get; set; }
    }
}