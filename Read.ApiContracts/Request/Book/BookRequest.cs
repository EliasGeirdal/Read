namespace Read.ApiContracts.Response.Books.CreateBook;

public class BookRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}