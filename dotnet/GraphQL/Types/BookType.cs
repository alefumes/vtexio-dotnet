using GettingStarted.DataSources.Authors;
using GettingStarted.DataSources.Books;
using GettingStarted.Model;
using GraphQL;
using GraphQL.Types;

namespace dotnet.GraphQL.Types
{
    [GraphQLMetadata("BookType")]
    public class BookType : ObjectGraphType<Book>
    {
        public BookType(IBooksDataSource booksDataSource, IAuthorsDataSource authorsDataSource)
        {
            Name = "Book";

            Field(b => b.Id).Description("The id of the book.");
            Field(b => b.Name).Description("The name of the book.");

            Field<AuthorType>(
                "author",
                resolve: context => authorsDataSource.GetAuthor(context.Source.AuthorId)
            );
        }
    }
}