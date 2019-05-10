using GettingStarted.DataSources.Authors;
using GettingStarted.DataSources.Books;
using Microsoft.AspNetCore.Http;
using GraphQL;
using GraphQL.Types;
using dotnet.GraphQL.Types;

namespace GettingStarted.GraphQL
{
    [GraphQLMetadata("Query")]
    public class Query : ObjectGraphType<object>
    {
        public Query(IBooksDataSource booksDataSource, IAuthorsDataSource authorsDataSource)
        {
            Name = "Query";

            Field<BookType>(
                "book",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the book" }
                ),
                resolve: context => booksDataSource.GetBook(context.GetArgument<int>("id"))
            );

            Field<ListGraphType<BookType>>(
                "books",
                resolve: context => booksDataSource.GetBooks()
            );

            Field<AuthorType>(
                "author",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the author" }
                ),
                resolve: context => authorsDataSource.GetAuthor(context.GetArgument<int>("id"))
            );

            Field<ListGraphType<AuthorType>>(
                "authors",
                resolve: context => authorsDataSource.GetAuthors()
            );
        }
    }
}