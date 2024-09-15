namespace FlavorVerse.Common.Grid;

public class PaginatedList<T>
{
    public int TotalCount { get; set; }

    public List<T> Items { get; set; } = [];
}