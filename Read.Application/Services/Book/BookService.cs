using Read.Application.Models;

namespace Read.Application.Services.Book;

public class BookService : IBookService
{
    public Task<bool> CreateAsync(Models.Book movie, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Book?> GetByIdAsync(Guid id, Guid? userid = default, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Book?> GetBySlugAsync(string slug, Guid? userid = default, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Models.Book>> GetAllAsync(GetAllBooksOptions options, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<Models.Book?> UpdateAsync(Models.Book movie, Guid? userid = default, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCountAsync(string? title, string? author, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}