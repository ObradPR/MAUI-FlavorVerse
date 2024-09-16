using FlavorVerse.BusinessLogic.Dto;
using FlavorVerse.Common.Grid;

namespace FlavorVerse.Interfaces;

public interface IRecipeService
{
    PaginatedList<RecipeDto> GetList(QueryParams? queryParams = null);

    RecipeDto GetById(Guid id);
}