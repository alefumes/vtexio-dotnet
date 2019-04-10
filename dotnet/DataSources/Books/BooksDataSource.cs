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
                new Book(1, "Harry Potter", 1),
                new Book(2, "Lord of the Rings: The Fellowship of the Ring", 2),
                new Book(3, "Lord of the Rings: The Two Towers", 2),
                new Book(4, "Lord of the Rings: The Return of the King", 2),
                new Book(5, "Song of Ice and Fire", 3),
                new Book(6, "Saxon Tales", 4),
                new Book(7, "Genghis Khan", 5),
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
    }
}