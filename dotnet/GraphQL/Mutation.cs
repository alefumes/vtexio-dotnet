using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GettingStarted.DataSources.Books;
using GettingStarted.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using GettingStarted.Utils;

namespace GettingStarted.GraphQL
{
    [GraphQLMetadata("Mutation")]
    public class Mutation
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
        public Book NewBook(ResolveFieldContext context, Book book)
        {
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