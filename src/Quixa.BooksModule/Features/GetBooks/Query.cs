using System.Data;
using Dapper;
using Quixa.BooksModule.Dtos;

namespace Quixa.BooksModule.Features.GetBooks;

internal static class Command
{
    private static readonly string _getBooks = @$"
SELECT
    {nameof(BookDto.Id)},
    {nameof(BookDto.Title)}
FROM
    Books
";

    public static Task<IEnumerable<BookDto>> GetBooksAsync(this IDbConnection db, CancellationToken cancellationToken)
        => db.QueryAsync<BookDto>(new CommandDefinition(_getBooks, cancellationToken: cancellationToken));
}
