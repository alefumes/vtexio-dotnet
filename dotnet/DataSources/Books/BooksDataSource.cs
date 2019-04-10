using System;
using System.Collections.Generic;
using System.Linq;
using GettingStarted.Model;

namespace GettingStarted.DataSources.Books
{
    public class BooksDataSource : IBooksDataSource
    {
        private List<Book> books { get; set; }
        public BooksDataSource()
        {
            books = new List<Book>()
            {
                new Book { Id = 1, Name = "Harry Potter", AuthorId = 1 },
                new Book { Id = 2, Name = "Lord of the Rings: The Fellowship of the Ring", AuthorId = 2 },
                new Book { Id = 3, Name = "Lord of the Rings: The Two Towers", AuthorId = 2 },
                new Book { Id = 4, Name = "Lord of the Rings: The Return of the King", AuthorId = 2 },
                new Book { Id = 5, Name = "Song of Ice and Fire", AuthorId = 3 },
                new Book { Id = 6, Name = "Saxon Tales", AuthorId = 4 },
                new Book { Id = 7, Name = "Genghis Khan", AuthorId = 5 },
            };
        }

        public List<Book> GetBooks()
        {
            return books;
        }

        public Book GetBook(int id)
        {
            return books.Where(b => b.Id == id).FirstOrDefault();
        }

        public Book NewBook(Book book)
        {
            if (book == null)
            {
                return null;
            }

            book.Id = GetNewId();
            books.Add(book);
            return book;
        }

        public Book EditBook(Book book)
        {
            if (book == null)
            {
                return null;
            }

            var existingBook = books.Where(b => b.Id == book.Id).SingleOrDefault();
            if (existingBook == null)
            {
                return null;
            }

            existingBook.Name = book.Name;
            existingBook.AuthorId = book.AuthorId;

            return existingBook;
        }

        public bool DeleteBook(int id)
        {
            var count = books.RemoveAll(b => b.Id == id);
            return count > 0;
        }

        private int GetNewId()
        {
            var lastBook = books.LastOrDefault();
            return lastBook != null ? lastBook.Id++ : 1;
        }

        public List<Book> GetBooksByAuthor(int authorId)
        {
            return books.Where(b => b.AuthorId == authorId).ToList();
        }
    }
}