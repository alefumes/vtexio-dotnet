using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using service.dataSources.books;
using service.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using service.Utils;

namespace service.graphql
{
    public class Mutation : IGraphQLOperation
    {
        private readonly IBooksDataSource booksDataSource;
        public Mutation(IBooksDataSource booksDataSource)
        {
            this.booksDataSource = booksDataSource;
        }

        [GraphQLMetadata("editBook")]
        public Book EditBook(int id, Book book)
        {
            if (book == null)
            {
                return null;
            }

            var existingBook = booksDataSource.GetBooks().Where(b => b.Id == id).SingleOrDefault();
            if (existingBook == null)
            {
                return null;
            }

            existingBook.Name = book.Name;
            existingBook.AuthorId = book.AuthorId;

            return existingBook;
        }

        [GraphQLMetadata("newBook")]
        public Book NewBook(ResolveFieldContext context, IOContext ioContext, Book book)
        {
            if (ioContext != null && ioContext.HttpContext != null)
            {
                string headers = $"account: {ioContext.Account}\nworkspace: {ioContext.Workspace}";
                Console.WriteLine(headers);
            }

            if (book == null)
            {
                return null;
            }

            var books = booksDataSource.GetBooks();
            book.Id = books.Count;
            books.Add(book);
            return book;
        }

        [GraphQLMetadata("deleteBook")]
        public bool DeleteBook(int id)
        {
            var count = booksDataSource.GetBooks().RemoveAll(b => b.Id == id);
            return count > 0;
        }
    }
}