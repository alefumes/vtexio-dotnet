using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GettingStarted.DataSources.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using GettingStarted.Utils;
using GettingStarted.GraphQL.Types;
using GettingStarted.Mappers;

namespace GettingStarted.GraphQL
{
    public partial class Mutation
    {
        [GraphQLMetadata("editBook")]
        public Book EditBook(int id, BookInput book)
        {
            if (book == null)
            {
                return null;
            }

            var existingBook = booksDataSource.GetBook(id);
            if (existingBook == null)
            {
                return null;
            }

            existingBook.Name = book.Name;
            existingBook.AuthorId = book.AuthorId;

            var author = authorsDataSource.GetAuthor(book.AuthorId);

            return BookMapper.ConvertModelBookToGraphQLBook(existingBook, author);
        }

        [GraphQLMetadata("newBook")]
        public Book NewBook(ResolveFieldContext context, BookInput book)
        {
            if (book == null)
            {
                return null;
            }

            var newBook = BookMapper.ConvertBookInputToModelBook(book);
            newBook = booksDataSource.NewBook(newBook);

            return BookMapper.ConvertModelBookToGraphQLBook(newBook, null);
        }

        [GraphQLMetadata("deleteBook")]
        public bool DeleteBook(int id)
        {
            return booksDataSource.DeleteBook(id);
        }
    }
}