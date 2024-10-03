using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Quixa.BooksModule.Dtos;

namespace Quixa.BooksModule.Features.GetBooks;

internal static class Endpoint
{
    public static IEndpointRouteBuilder MapGetBooksEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet(
                "/",
                async (
                    IDbConnection db,
                    CancellationToken ct
                ) =>
                {
                    return Results.Ok(await db.GetBooksAsync(ct));
                })
            .Produces<IEnumerable<BookDto>>()
            .WithTags("Books");
        return endpoints;
    }
}
