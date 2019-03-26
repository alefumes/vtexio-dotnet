using System.Collections.Generic;
using service.Model;

namespace service.dataSources.authors
{
    public class AuthorsDataSource : IAuthorsDataSource
    {
        private List<Author> authors { get; set; }
        public AuthorsDataSource()
        {
            authors = new List<Author>()
            {
                new Author(1, "JK Rowling", "rowling@gmail.com" ),
                new Author(2, "Tolkien", "tolkien@gmail.com"),
                new Author(3, "Martin", "martin@gmail.com"),
                new Author(4, "Bernard Cornwell", "cornwell@gmail.com"),
                new Author(5, "Conn Iggulden", "iggulden@gmail.com"),
            };
        }

        public List<Author> GetAuthors()
        {
            return authors;
        }
    }
}