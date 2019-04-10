using System.Collections.Generic;
using System.Linq;
using GettingStarted.Model;

namespace GettingStarted.DataSources.Authors
{
    public class AuthorsDataSource : IAuthorsDataSource
    {
        private List<Author> authors { get; set; }
        public AuthorsDataSource()
        {
            authors = new List<Author>()
            {
                new Author { Id = 1, Name = "JK Rowling", Email = "rowling@gmail.com" },
                new Author { Id = 2, Name = "Tolkien", Email = "tolkien@gmail.com" },
                new Author { Id = 3, Name = "Martin", Email = "martin@gmail.com" },
                new Author { Id = 4, Name = "Bernard Cornwell", Email = "cornwell@gmail.com" },
                new Author { Id = 5, Name = "Conn Iggulden", Email = "iggulden@gmail.com" },
            };
        }

        public List<Author> GetAuthors()
        {
            return authors;
        }

        public Author GetAuthor(int id)
        {
            return authors.Where(b => b.Id == id).FirstOrDefault();
        }

        public Author NewAuthor(Author author)
        {
            if (author == null)
            {
                return null;
            }

            author.Id = GetNewId();
            authors.Add(author);
            return author;
        }

        public Author EditAuthor(Author author)
        {
            if (author == null)
            {
                return null;
            }

            var existingAuthor = authors.Where(b => b.Id == author.Id).SingleOrDefault();
            if (existingAuthor == null)
            {
                return null;
            }

            existingAuthor.Name = author.Name;
            existingAuthor.Email = author.Email;

            return existingAuthor;
        }

        public bool DeleteAuthor(int id)
        {
            var count = authors.RemoveAll(b => b.Id == id);
            return count > 0;
        }

        private int GetNewId()
        {
            var lastAuthor = authors.LastOrDefault();
            return lastAuthor != null ? lastAuthor.Id++ : 1;
        }
    }
}