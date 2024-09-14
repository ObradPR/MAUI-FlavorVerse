using FluentValidation.Results;

namespace FlavorVerse.Extensions
{
    public static partial class Extensions
    {
        public static string GetError(this ValidationResult result, string property)
        {
            return result.Errors.FirstOrDefault(x => x.PropertyName == property + ".Value")?.ErrorMessage;
        }

        public static bool BeInThePast(DateOnly? date)
        {
            if (!date.HasValue)
            {
                return false;
            }

            return date < DateOnly.FromDateTime(DateTime.Now.Date);
        }

        public static bool BeAValidAge(DateOnly? date)
        {
            if (!date.HasValue)
            {
                return false;
            }

            return date > DateOnly.FromDateTime(DateTime.Now.AddYears(-100)) && date < DateOnly.FromDateTime(DateTime.Now.Date.AddYears(-12));
        }
    }
}