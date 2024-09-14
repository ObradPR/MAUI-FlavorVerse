namespace FlavorVerse.BusinessLogic.Dto
{
    public class UserDto : TokenDto
    {
        public string Username { get; set; } = string.Empty;

        public Guid Id { get; set; }
    }
}