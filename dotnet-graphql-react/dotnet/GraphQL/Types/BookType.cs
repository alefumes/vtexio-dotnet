using GettingStarted.DataSources.Books;
using GettingStarted.Model;
using GraphQL;
using GraphQL.Types;

namespace dotnet.GraphQL.Types
{
    [GraphQLMetadata("BookType")]
    public class BookType : ObjectGraphType<Book>
    {
        public BookType(IBooksDataSource booksDataSource)
        {
            Name = "Book";

            Field(b => b.Id).Description("The id of the book.");
            Field(b => b.CacheId).Description("The cache id of the book.");
            Field(b => b.Name).Description("The name of the book.");
            Field(b => b.Authors).Description("The authors of the book.");
        }
    }
}