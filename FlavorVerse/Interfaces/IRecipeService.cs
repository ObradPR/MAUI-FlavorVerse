using FlavorVerse.BusinessLogic.Dto;
using FlavorVerse.Common.Grid;

namespace FlavorVerse.Interfaces;

public interface IRecipeService
{
    List<RecipeDto> GetList(QueryParams queryParams);
}