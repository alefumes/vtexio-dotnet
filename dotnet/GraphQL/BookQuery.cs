using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL;
using GraphQL.Types;
using GettingStarted.DataSources.Books;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using GettingStarted.GraphQL.Types;
using GettingStarted.Mappers;

namespace GettingStarted.GraphQL
{
    public partial class Query
    {
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
                return null;

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
    }
}