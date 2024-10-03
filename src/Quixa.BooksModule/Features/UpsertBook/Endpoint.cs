using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Quixa.BooksModule.Dtos;

namespace Quixa.BooksModule.Features.UpsertBook;

internal static class Endpoint
{
    public static IEndpointRouteBuilder MapUpsertBookEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost(
                "/",
                async (
                    BookDto book,
                    IDbConnection db,
                    CancellationToken ct
                ) =>
                {
                    await db.UpsertBookAsync(book, ct);
                    return Results.NoContent();
                })
            .Produces(StatusCodes.Status204NoContent)
            .WithTags("Books");
        return endpoints;
    }
}
