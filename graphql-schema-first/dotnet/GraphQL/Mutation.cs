using GraphQL;
using GraphQL.Types;
using GettingStarted.DataSources.Books;
using GettingStarted.Model;
using Microsoft.AspNetCore.Http;
using GettingStarted.DataSources.Authors;
using GettingStarted.GraphQL.Types;
using GettingStarted.Mappers;

namespace GettingStarted.GraphQL
{
    [GraphQLMetadata("Mutation")]
    public class Mutation
    {
        private readonly IBooksDataSource booksDataSource;
        private readonly IAuthorsDataSource authorsDataSource;
        private readonly IHttpContextAccessor contextAccessor;
        public Mutation(IBooksDataSource booksDataSource, IAuthorsDataSource authorsDataSource, IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            this.authorsDataSource = authorsDataSource;
            this.booksDataSource = booksDataSource;
        }

        [GraphQLMetadata("editBook")]
        public Types.Book EditBook(int id, BookInput book)
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
        public Types.Book NewBook(ResolveFieldContext context, BookInput book)
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

        [GraphQLMetadata("editAuthor")]
        public Types.Author EditAuthor(int id, AuthorInput author)
        {
            if (author == null)
            {
                return null;
            }

            var existingAuthor = authorsDataSource.GetAuthor(id);
            if (existingAuthor == null)
            {
                return null;
            }

            existingAuthor.Name = author.Name;
            existingAuthor.Email = author.Email;

            var books = booksDataSource.GetBooksByAuthor(id);

            return AuthorMapper.ConvertModelAuthorToGraphQLAuthor(existingAuthor, books);
        }

        [GraphQLMetadata("newAuthor")]
        public Types.Author NewAuthor(ResolveFieldContext context, AuthorInput author)
        {
            if (author == null)
            {
                return null;
            }

            var newAuthor = AuthorMapper.ConvertAuthorInputToModelAuthor(author);
            newAuthor = authorsDataSource.NewAuthor(newAuthor);

            return AuthorMapper.ConvertModelAuthorToGraphQLAuthor(newAuthor, null);
        }

        [GraphQLMetadata("deleteAuthor")]
        public bool DeleteAuthor(int id)
        {
            return authorsDataSource.DeleteAuthor(id);
        }
    }
}