using Read.Application.Models;

namespace Read.Application.Repository.Book;

public class BookRepository : IBookRepository
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

    public Task<bool> UpdateAsync(Models.Book movie, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetCountAsync(string? title, int? yearOfRelease, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}