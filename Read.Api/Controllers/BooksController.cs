using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Read.Api.Auth;
using Read.Api.Mapping;
using Read.ApiContracts.Request.Book;
using Read.ApiContracts.Response.Books.CreateBook;
using Read.ApiContracts.Response.Validation;
using Read.Application.Services.Book;

namespace Read.Api.Controllers;

[ApiController]
[ApiVersion(1.0)]
public class BookController : ControllerBase
{
    private readonly IOutputCacheStore _outputCacheStore;
    private readonly IBookService _bookService;

    public BookController(IOutputCacheStore outputCacheStore, IBookService bookService)
    {
        _outputCacheStore = outputCacheStore;
        _bookService = bookService;
    }
    
    [Authorize(AuthConstants.TrustedMemberPolicyName)]
    [HttpPost(ApiEndpoints.Books.Create)]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody]BookRequest request,
        CancellationToken token)
    {
        var book = request.MapToBook();
        await _bookService.CreateAsync(book, token);
        await _outputCacheStore.EvictByTagAsync("movies", token);
		var bookResponse = book.MapToResponse();
        return CreatedAtAction(nameof(GetV1), new { idOrSlug = book.Id }, bookResponse);
    }
     
    [HttpGet(ApiEndpoints.Books.Get)]
    [OutputCache(PolicyName = "BookCache")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetV1([FromRoute] string idOrSlug,
        CancellationToken token)
    {
        var userId = HttpContext.GetUserId();
        
        var book = Guid.TryParse(idOrSlug, out var id)
            ? await _bookService.GetByIdAsync(id, userId, token)
            : await _bookService.GetBySlugAsync(idOrSlug, userId, token);
        if (book is null)
        {
            return NotFound();
        }
    
        var response = book.MapToResponse();
        return Ok(response);
    }
    
    [HttpGet(ApiEndpoints.Books.GetAll)]
    [OutputCache(PolicyName = "BookCache")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetAllBooksRequest request, CancellationToken token)
    {
        var userId = HttpContext.GetUserId();
        var options = request.MapToOptions()
            .WithUser(userId);
        var books = await _bookService.GetAllAsync(options, token);
        var booksCount = await _bookService.GetCountAsync(options.Title, options.Author, token);
        var moviesResponse = books.MapToResponse(request.Page, request.PageSize, booksCount);
        return Ok(moviesResponse);
    }
    
    [Authorize(AuthConstants.TrustedMemberPolicyName)]
    [HttpPut(ApiEndpoints.Books.Update)]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute]Guid id,
        [FromBody]UpdateBookRequest request,
        CancellationToken token) 
    {
        var book = request.MapToBook(id);
        var userId = HttpContext.GetUserId();
        var updatedBook = await _bookService.UpdateAsync(book, userId, token);
        if (updatedBook is null)
        {
            return NotFound();
        }
    
        await _outputCacheStore.EvictByTagAsync("books", token);
        var response = updatedBook.MapToResponse();
        return Ok(response);
    }
    
    [Authorize(AuthConstants.AdminUserPolicyName)]
    [HttpDelete(ApiEndpoints.Books.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id,
        CancellationToken token)
    {
        var deleted = await _bookService.DeleteByIdAsync(id, token);
        if (!deleted)
        {
            return NotFound();
        }
    
        await _outputCacheStore.EvictByTagAsync("books", token);
        return Ok();
    }
}