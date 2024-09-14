namespace FlavorVerse.Common.Grid;

public class QueryParams : BaseGridParams
{
    public string? SearchTerm { get; set; }

    public string? FilterCriteria { get; set; }

    public string? FilterDirection { get; set; }
}