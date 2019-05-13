using GettingStarted.DataSources.Authors;
using GettingStarted.DataSources.Books;
using GettingStarted.Model;
using GraphQL;
using GraphQL.Types;

namespace dotnet.GraphQL.Types
{
    [GraphQLMetadata("AuthorType")]
    public class AuthorType : ObjectGraphType<Author>
    {
        public AuthorType(IBooksDataSource booksDataSource, IAuthorsDataSource authorsDataSource)
        {
            Name = "Author";

            Field(a => a.Id).Description("The id of the author.");
            Field(a => a.Name).Description("The name of the author.");
            Field(a => a.Email).Description("The email of the author.");

            Field<ListGraphType<BookType>>(
                "books",
                resolve: context => booksDataSource.GetBooksByAuthor(context.Source.Id)
            );
        }
    }
}