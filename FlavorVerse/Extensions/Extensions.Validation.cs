using FluentValidation.Results;

namespace FlavorVerse.Extensions
{
    public static partial class Extensions
    {
        public static string GetError(this ValidationResult result, string property)
        {
            return result.Errors.FirstOrDefault(x => x.PropertyName == property + ".Value")?.ErrorMessage;
        }
    }
}