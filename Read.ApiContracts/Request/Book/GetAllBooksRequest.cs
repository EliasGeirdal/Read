namespace Read.ApiContracts.Request.Book;

public class GetAllBooksRequest : PagedRequest
{
    public required string? Title { get; init; }

    public required string? Author { get; init; }
    
    public required string? SortBy { get; init; }
}