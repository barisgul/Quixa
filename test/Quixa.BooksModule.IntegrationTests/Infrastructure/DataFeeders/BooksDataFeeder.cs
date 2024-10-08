using Dapper;
using System.Data;
using Quixa.BooksModule.Dtos;

namespace Quixa.BooksModule.IntegrationTests.Infrastructure.DataFeeders
{
    internal static class BooksDataFeeder
    {
        public static void Feed(IDbConnection db)
        {
            db.Execute(@$"
INSERT INTO Books ({nameof(BookDto.Id)}, {nameof(BookDto.Title)})
    VALUES(1, 'C# book');");

            db.Execute(@$"
INSERT INTO Books ({nameof(BookDto.Id)}, {nameof(BookDto.Title)})
    VALUES(2, '.NET book');");
        }
    }
}
