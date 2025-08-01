using Read.ApiContracts.Request.Auth;
using Read.ApiContracts.Request.Book;
using Read.ApiContracts.Request.User;
using Read.ApiContracts.Response.Auth;
using Read.ApiContracts.Response.Books.CreateBook;
using Read.Application.Models;
using Read.Application.Models.Dto;

namespace WebApi.Mapping;

public static class ContractMapping
{
    public static Book MapToBook(this BookRequest request)
    {
        return new Book
        {
            Id = request.Id,
            Title = request.Title,
            Author = request.Author,
            Image = request.Image,
        };
    }

    public static BookResponse MapToResponse(this Book book)
    {
        return new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author
        };
    }

    public static BooksResponse MapToResponse(this IEnumerable<Book> books,
        int page, int pageSize, int totalCount)
    {
        return new BooksResponse
        {
            Items = books.Select(MapToResponse),
            Page = page,
            PageSize = pageSize,
            Total = totalCount
        };
    }

    public static GetAllBooksOptions MapToOptions(this GetAllBooksRequest request)
    {
        return new GetAllBooksOptions
        {
            SortField = request.SortBy?.Trim('+', '-'),
            SortOrder = request.SortBy is null ? SortOrder.Unsorted :
                request.SortBy.StartsWith('-') ? SortOrder.Descending : SortOrder.Ascending,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }

    public static GetAllBooksOptions WithUser(this GetAllBooksOptions options,
        Guid? userId)
    {
        options.UserId = userId;
        return options;
    }

    public static Book MapToBook(this UpdateBookRequest request, Guid id)
    {
        return new Book
        {
            Id = id
        };
    }

    public static UserDto MapToUser(this UserRequest request)
    {
        return new UserDto
        {
            Password = request.Password,
            Username = request.Username,
        };
    }

    public static RefreshTokenDto MapToRefreshToken(this RefreshTokenRequest request)
    {
        return new RefreshTokenDto
        {
            RefreshToken = request.RefreshToken,
            UserId = request.UserId,
        };
    }

    public static TokenResponse MapToTokenResponse(this TokenDto token)
    {
        return new TokenResponse
        {
            RefreshToken = token.RefreshToken,
            AccessToken = token.AccessToken
        };
    }

    public static UserResponse MapToUserResponse(this User user)
    {
        return new UserResponse
        {
            RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
            Username = user.Username,
            Id = user.Id,
            RefreshToken = user.RefreshToken,
            Role = user.Role,
            PasswordHash = user.PasswordHash,
        };
    }
}