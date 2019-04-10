using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL;
using GraphQL.Types;
using GettingStarted.DataSources.Authors;
using GettingStarted.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GettingStarted.GraphQL
{
    public partial class Query
    {
        [GraphQLMetadata("authors")]
        public List<Author> GetAuthors()
        {
            if (context != null)
                Console.WriteLine(context.HttpContext.Request.Path);

            return authorsDataSource.GetAuthors();
        }

        [GraphQLMetadata("author")]
        public GettingStarted.GraphQL.Types.Author GetAuthor(ResolveFieldContext context, int id)
        {
            var author = authorsDataSource.GetAuthors().Where(b => b.Id == id).SingleOrDefault();
            if (author == null)
                return null;

            var result = new GettingStarted.GraphQL.Types.Author()
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email
            };

            if (context.SubFields.ContainsKey("books"))
            {
                result.Books = new List<GettingStarted.GraphQL.Types.Book>();

                var books = booksDataSource.GetBooks().Where(b => b.AuthorId == id);
                foreach(var book in books)
                {
                    result.Books.Add(new GettingStarted.GraphQL.Types.Book() { Id = book.Id, Name = book.Name, Author = result });
                }
            }

            return result;
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
    }
}