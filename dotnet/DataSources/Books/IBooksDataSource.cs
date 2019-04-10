using System.Collections.Generic;
using GettingStarted.Model;

namespace GettingStarted.DataSources.Books
{
    public interface IBooksDataSource
    {
        List<Book> GetBooks();
        Book GetBook(int id);
        Book NewBook(Book book);
        Book EditBook(Book book);
        bool DeleteBook(int id);
    }
}