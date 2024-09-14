using FlavorVerse.BusinessLogic.Dto;
using FlavorVerse.Common.Grid;
using FlavorVerse.Interfaces;
using RestSharp;

namespace FlavorVerse.Services;

public class RecipeService : IRecipeService
{
    public List<RecipeDto> GetList(QueryParams queryParams)
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

        var res = Api.Client.Execute<List<RecipeDto>>(req);

        return res.IsSuccessful
            ? res.Data ?? ([])
            : [];
    }
}