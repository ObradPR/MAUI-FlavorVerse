using FlavorVerse.BusinessLogic.Dto;
using FlavorVerse.Common.Grid;
using FlavorVerse.Interfaces;
using RestSharp;

namespace FlavorVerse.Services;

public class RecipeService : IRecipeService
{
    public Task AddAsync(AddRecipeDto data)
    {
        var req = new RestRequest("recipe", Method.Post);

        // Add form data to the request
        req.AddParameter("Title", data.Title);
        req.AddParameter("Description", data.Description);
        req.AddParameter("CookingTime", data.CookingTime);
        req.AddParameter("Servings", data.Servings);
        req.AddParameter("Instructions", data.Instructions);
        if (data.Protein.HasValue)
            req.AddParameter("Protein", data.Protein.Value);
        if (data.Carbohydrates.HasValue)
            req.AddParameter("Carbohydrates", data.Carbohydrates.Value);
        if (data.Fat.HasValue)
            req.AddParameter("Fat", data.Fat.Value);
        if (data.Fiber.HasValue)
            req.AddParameter("Fiber", data.Fiber.Value);
        if (data.GlutenFree.HasValue)
            req.AddParameter("GlutenFree", data.GlutenFree.Value);
        if (data.DairyFree.HasValue)
            req.AddParameter("DairyFree", data.DairyFree.Value);
        if (data.Vegetarian.HasValue)
            req.AddParameter("Vegetarian", data.Vegetarian.Value);
        if (data.Vegan.HasValue)
            req.AddParameter("Vegan", data.Vegan.Value);
        req.AddParameter("MealTypeId", data.MealTypeId);
        req.AddParameter("DifficultyCookingId", data.DifficultyCookingId);
        req.AddParameter("MainCuisineId", data.MainCuisineId);
        req.AddParameter("MainCategoryId", data.MainCategoryId);

        // Add file if it exists
        if (data.Image != null && data.Image.Length > 0)
        {
            // Add file using Stream overload
            req.AddFile("Image",
                        () => data.Image.OpenReadStream(), // Function returning the Stream
                        data.Image.FileName,               // File name
                        "image/jpeg");                     // MIME type
        }

        // Execute the request
        var res = Api.Client.Execute(req);

        if (!res.IsSuccessful)
        {
            // Handle failure (e.g., log errors or throw exceptions)
            var errorMessage = res.ErrorMessage;
            throw new Exception($"API request failed: {errorMessage}");
        }

        return Task.CompletedTask;
    }

    public RecipeDto GetById(Guid id)
    {
        var req = new RestRequest($"recipe/{id}", Method.Get);

        var res = Api.Client.Execute<RecipeDto>(req);

        return res.IsSuccessful
            ? res.Data
            : new RecipeDto();
    }

    public PaginatedList<RecipeDto> GetList(QueryParams? queryParams = null)
    {
        var req = new RestRequest("recipe", Method.Get);

        var pageSize = queryParams?.PageSize;
        if (pageSize != null)
            req.AddParameter(nameof(pageSize), pageSize, ParameterType.QueryString);

        var pageNumber = queryParams?.PageNumber;
        if (pageNumber != null)
            req.AddParameter(nameof(pageNumber), pageNumber, ParameterType.QueryString);

        var searchTerm = queryParams?.SearchTerm;
        if (searchTerm != null)
            req.AddParameter(nameof(searchTerm), searchTerm, ParameterType.QueryString);

        var filterCriteria = queryParams?.FilterCriteria;
        if (filterCriteria != null)
            req.AddParameter(nameof(filterCriteria), filterCriteria, ParameterType.QueryString);

        var filterDirection = queryParams?.FilterDirection;
        if (filterDirection != null)
            req.AddParameter(nameof(filterDirection), filterDirection, ParameterType.QueryString);

        var res = Api.Client.Execute<PaginatedList<RecipeDto>>(req);

        return res.IsSuccessful
            ? res.Data
            : new PaginatedList<RecipeDto>();
    }
}