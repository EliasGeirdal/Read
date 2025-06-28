using Read.Application.Models;

namespace Read.Application.Services.Book;

public interface IBookService
{
    Task<bool> CreateAsync(Models.Book movie, CancellationToken token = default);
    
    Task<Models.Book?> GetByIdAsync(Guid id, Guid? userid = default, CancellationToken token = default);
    
    Task<Models.Book?> GetBySlugAsync(string slug, Guid? userid = default, CancellationToken token = default);
    
    Task<IEnumerable<Models.Book>> GetAllAsync(GetAllBooksOptions options, CancellationToken token = default);
    
    Task<Models.Book?> UpdateAsync(Models.Book movie, Guid? userid = default, CancellationToken token = default);
    
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);

    Task<int> GetCountAsync(string? title, string? author, CancellationToken token = default);
}