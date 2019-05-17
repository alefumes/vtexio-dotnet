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
                new Book { Id = 1, CacheId = 1, Name = "Harry Potter", Authors = new string[] { "JK Rowling" } },
                new Book { Id = 2, CacheId = 2, Name = "Lord of the Rings: The Fellowship of the Ring", Authors = new string[] { "Tolkien" } },
                new Book { Id = 3, CacheId = 3, Name = "Lord of the Rings: The Two Towers", Authors = new string[] { "Tolkien" } },
                new Book { Id = 4, CacheId = 4, Name = "Lord of the Rings: The Return of the King", Authors = new string[] { "Tolkien" } },
                new Book { Id = 5, CacheId = 5, Name = "Song of Ice and Fire", Authors = new string[] { "Martin" } },
                new Book { Id = 6, CacheId = 6, Name = "Saxon Tales", Authors = new string[] { "Bernard Cornwell" } },
                new Book { Id = 7, CacheId = 7, Name = "Genghis Khan", Authors = new string[] { "Conn Iggulden" } },
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
            book.CacheId = book.Id;
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
            existingBook.Authors = book.Authors;

            return existingBook;
        }

        public bool DeleteBook(int id)
        {
            var count = books.RemoveAll(b => b.Id == id);
            return count > 0;
        }

        private int GetNewId()
        {
            var lastBook = books.OrderBy(b => b.Id).LastOrDefault();
            return lastBook != null ? lastBook.Id + 1 : 1;
        }
    }
}