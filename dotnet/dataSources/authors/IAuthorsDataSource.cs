using System.Collections.Generic;
using service.Model;

namespace service.dataSources.authors
{
    public interface IAuthorsDataSource
    {
        List<Author> GetAuthors();
    }
}