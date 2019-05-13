using GettingStarted.DataSources.Authors;
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
        public Mutation(IBooksDataSource booksDataSource, IAuthorsDataSource authorsDataSource)
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
                "deleteBook",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> {Name = "id"}
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return booksDataSource.DeleteBook(id);
                });

            Field<AuthorType>(
                "newAuthor",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AuthorInputType>> {Name = "author"}
                ),
                resolve: context =>
                {
                    var author = context.GetArgument<Author>("author");
                    return authorsDataSource.NewAuthor(author);
                });

            Field<AuthorType>(
                "editAuthor",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> {Name = "id"},
                    new QueryArgument<NonNullGraphType<AuthorInputType>> {Name = "author"}
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    var author = context.GetArgument<Author>("author");
                    author.Id = id;
                    return authorsDataSource.EditAuthor(author);
                });

            Field<BooleanGraphType>(
                "deleteAuthor",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> {Name = "id"}
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return authorsDataSource.DeleteAuthor(id);
                });
        }
    }
}