using System.Collections.Generic;
using service.Model;

namespace service.dataSources.books
{
    public interface IBooksDataSource
    {
        List<Book> GetBooks();
    }
}