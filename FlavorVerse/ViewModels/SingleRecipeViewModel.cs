using FlavorVerse.BusinessLogic.Dto;
using FlavorVerse.Common;
using FlavorVerse.Extensions;
using FlavorVerse.Interfaces;
using FlavorVerse.Services;
using RestSharp;
using System.Windows.Input;

namespace FlavorVerse.ViewModels
{
    public class SingleRecipeViewModel
    {
        private readonly IRecipeService _recipeService;

        public MProp<RecipeDto> Recipe { get; set; } = new MProp<RecipeDto>();

        public bool IsLogged { get; set; }

        // Properties for the comment and rating
        private string _newCommentText;

        public MProp<string> NewCommentText { get; set; } = new MProp<string>();

        public MProp<double> RatingValue { get; set; } = new MProp<double>();

        public ICommand SubmitReviewCommand { get; set; }

        public SingleRecipeViewModel(RecipeDto recipe)
        {
            _recipeService = new RecipeService();
            RatingValue.Value = 0;

            var user = SecureStorage.Default.GetUser();

            if (user != null)
            {
                IsLogged = true;
            }
            else
            {
                IsLogged = false;
            }

            Recipe.Value = recipe;
            SubmitReviewCommand = new Command(SubmitReview);
        }

        private void SubmitReview()
        {
            var data = new NewRatingDto
            {
                RecipeId = Recipe.Value.Id,
                RatingValue = (int)RatingValue.Value,
                Comment = NewCommentText.Value
            };

            var req = new RestRequest("rating", Method.Post);
            req.AddJsonBody(data);

            var res = Api.Client.Execute(req);

            if (res.IsSuccessful)
            {
                Recipe.Value = _recipeService.GetById(Recipe.Value.Id);

                RatingValue.Value = default;
                NewCommentText.Value = string.Empty;
            }
        }
    }
}