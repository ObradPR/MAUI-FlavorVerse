﻿namespace FlavorVerse.BusinessLogic.Dto
{
    public class SignUpDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;

        public DateOnly? DateOfBirth { get; set; }
    }
}