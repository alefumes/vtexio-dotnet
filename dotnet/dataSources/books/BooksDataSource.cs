using System.Collections.Generic;
using service.Model;

namespace service.dataSources.books
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
    }
}