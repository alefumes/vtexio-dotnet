using GettingStarted.DataSources.Books;
using Microsoft.AspNetCore.Http;
using GraphQL;
using GraphQL.Types;
using dotnet.GraphQL.Types;
using Newtonsoft.Json;
using dotnet.Markdown;

namespace GettingStarted.GraphQL
{
    [GraphQLMetadata("Query")]
    public class Query : ObjectGraphType<object>
    {
        public Query(IBooksDataSource booksDataSource)
        {
            Name = "Query";

            Field<BookType>(
                "book",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the book" }
                ),
                resolve: context => booksDataSource.GetBook(context.GetArgument<int>("id"))
            );

            Field<StringGraphType>(
                "source",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the markdown source" }
                ),
                resolve: context => MarkdownHelper.GetMarkdown(context.GetArgument<string>("id"))
            );

            Field<ListGraphType<BookType>>(
                "books",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "from", Description = "first pagination index" },
                    new QueryArgument<IntGraphType> { Name = "to", Description = "last pagination index" }
                ),
                resolve: context => booksDataSource.GetBooks()
            );

            Field<IntGraphType>(
                "total",
                resolve: context => booksDataSource.GetBooks().Count
            );
        }
    }
}