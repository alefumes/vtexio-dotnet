using GettingStarted.DataSources.Authors;
using GettingStarted.DataSources.Books;
using Microsoft.AspNetCore.Http;
using GraphQL;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;
using Newtonsoft.Json;
using GettingStarted.GraphQL.Types;
using GettingStarted.Mappers;

namespace GettingStarted.GraphQL
{
    [GraphQLMetadata("Query")]
    public class Query
    {
        private readonly IBooksDataSource booksDataSource;
        private readonly IAuthorsDataSource authorsDataSource;
        private readonly IHttpContextAccessor contextAccessor;
        public Query(IBooksDataSource booksDataSource, IAuthorsDataSource authorsDataSource, IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            this.authorsDataSource = authorsDataSource;
            this.booksDataSource = booksDataSource;
        }

        [GraphQLMetadata("books")]
        public IEnumerable<Book> GetBooks(ResolveFieldContext context)
        {
            return booksDataSource.GetBooks()
            .Select(b => ConvertToGraphQLBook(b, ShouldIncludeAuthor(context)));
        }

        [GraphQLMetadata("book")]
        public Book GetBook(ResolveFieldContext context, int id)
        {
            var book = booksDataSource.GetBook(id);
            if (book == null)
            {
                return null;
            }

            return ConvertToGraphQLBook(book, ShouldIncludeAuthor(context));
        }

        [GraphQLMetadata("booksCount")]
        public int GetBooksCount()
        {
            return booksDataSource.GetBooks().Count;
        }

        [GraphQLMetadata("bookSource")]
        public string GetBookSource(ResolveFieldContext context, int id)
        {
            var book = GetBook(context, id);
            if (book == null)
            {
                return "could not find book";
            }

            return JsonConvert.SerializeObject(book);
        }

        private Book ConvertToGraphQLBook(Model.Book modelBook, bool includeAuthor = false)
        {
            var modelAuthor = includeAuthor ? authorsDataSource.GetAuthor(modelBook.AuthorId) : null;
            return BookMapper.ConvertModelBookToGraphQLBook(modelBook, modelAuthor);
        }

        private bool ShouldIncludeAuthor(ResolveFieldContext context) => context.SubFields?.ContainsKey("author") ?? false;

        [GraphQLMetadata("authors")]
        public IEnumerable<Author> GetAuthors(ResolveFieldContext context)
        {
            return authorsDataSource.GetAuthors()
            .Select(a => ConvertToGraphQLAuthor(a, ShouldIncludeBooks(context)));
        }

        [GraphQLMetadata("author")]
        public Author GetAuthor(ResolveFieldContext context, int id)
        {
            var author = authorsDataSource.GetAuthor(id);
            if (author == null)
            {
                return null;
            }

            return ConvertToGraphQLAuthor(author, ShouldIncludeBooks(context));
        }

        [GraphQLMetadata("authorsCount")]
        public int GetAuthorsCount()
        {
            return authorsDataSource.GetAuthors().Count;
        }

        [GraphQLMetadata("authorSource")]
        public string GetAuthorSource(ResolveFieldContext context, int id)
        {
            var author = GetAuthor(context, id);
            if (author == null)
            {
                return "could not find author";
            }

            return JsonConvert.SerializeObject(author);
        }

        private Author ConvertToGraphQLAuthor(Model.Author modelAuthor, bool includeBooks = false)
        {
            var modelBooks = includeBooks ? booksDataSource.GetBooksByAuthor(modelAuthor.Id) : null;
            return AuthorMapper.ConvertModelAuthorToGraphQLAuthor(modelAuthor, modelBooks);
        }

        private bool ShouldIncludeBooks(ResolveFieldContext context) => context.SubFields?.ContainsKey("books") ?? false;
    }
}