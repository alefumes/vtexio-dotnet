using System.Collections.Generic;
using GettingStarted.Model;

namespace GettingStarted.DataSources.Authors
{
    public interface IAuthorsDataSource
    {
        List<Author> GetAuthors();
        Author GetAuthor(int id);
        Author NewAuthor(Author author);
        Author EditAuthor(Author author);
        bool DeleteAuthor(int id);
    }
}