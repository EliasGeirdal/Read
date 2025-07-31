namespace Read.Application.Models;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public byte[] Image { get; set; }
    public int TotalPages { get; set; }
    public int PagesRead { get; set; }
}