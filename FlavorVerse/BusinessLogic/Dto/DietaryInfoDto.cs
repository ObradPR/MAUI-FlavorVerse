namespace FlavorVerse.BusinessLogic.Dto
{
    public class DietaryInfoDto
    {
        public int Id { get; set; }
        public bool GlutenFree { get; set; }
        public bool DairyFree { get; set; }
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
    }
}