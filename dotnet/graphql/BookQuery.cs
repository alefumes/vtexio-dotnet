using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL;
using GraphQL.Types;
using service.dataSources.books;
using service.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace service.graphql
{
    public partial class Query
    {
        [GraphQLMetadata("books")]
        public List<Book> GetBooks()
        {
            if (context != null)
                Console.WriteLine(context.HttpContext.Request.Path);

            return booksDataSource.GetBooks();
        }

        [GraphQLMetadata("book")]
        public service.dto.Book GetBook(ResolveFieldContext context, int id)
        {
            var book = booksDataSource.GetBooks().Where(b => b.Id == id).SingleOrDefault();
            if (book == null)
                return null;

            var result = new service.dto.Book()
            {
                Id = book.Id,
                Name = book.Name
            };

            if (context.SubFields.ContainsKey("author"))
            {
                var author = authorsDataSource.GetAuthors().Where(a => a.Id == book.AuthorId).SingleOrDefault();
                if (author != null)
                {
                    result.Author = new service.dto.Author()
                    {
                        Id = author.Id,
                        Name = author.Name,
                        Email = author.Email
                    };
                }
            }

            return result;
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
    }
}