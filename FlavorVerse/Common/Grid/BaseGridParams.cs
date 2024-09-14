namespace FlavorVerse.Common.Grid;

public class BaseGridParams
{
    private int _pageSize = Constants.DEFAULT_PAGE_SIZE;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value > Constants.MAX_PAGE_SIZE || value < Constants.DEFAULT_PAGE_SIZE)
            {
                _pageSize = Constants.DEFAULT_PAGE_SIZE;

                return;
            }

            _pageSize = value;
        }
    }
}