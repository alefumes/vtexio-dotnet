using GettingStarted.DataSources.Books;
using Microsoft.AspNetCore.Http;
using GraphQL;
using GraphQL.Types;
using dotnet.GraphQL.Types;
using Newtonsoft.Json;
using GettingStarted.GraphQL.Types;
using GettingStarted.Model;

namespace GettingStarted.GraphQL
{
    [GraphQLMetadata("Mutation")]
    public class Mutation : ObjectGraphType<object>
    {
        public Mutation(IBooksDataSource booksDataSource)
        {
            Name = "Mutation";

            Field<BookType>(
                "newBook",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BookInputType>> {Name = "book"}
                ),
                resolve: context =>
                {
                    var book = context.GetArgument<Book>("book");
                    return booksDataSource.NewBook(book);
                });

            Field<BookType>(
                "editBook",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> {Name = "id"},
                    new QueryArgument<NonNullGraphType<BookInputType>> {Name = "book"}
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var book = context.GetArgument<Book>("book");
                    book.Id = id;
                    return booksDataSource.EditBook(book);
                });

            Field<BooleanGraphType>(
                "delete",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> {Name = "id"}
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<string>("id");
                    if (int.TryParse(id, out int idValue))
                    {
                        return booksDataSource.DeleteBook(idValue);
                    }
                    return false;
                });
        }
    }
}