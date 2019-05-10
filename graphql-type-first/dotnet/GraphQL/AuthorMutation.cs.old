using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using GettingStarted.DataSources.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using GettingStarted.Utils;
using GettingStarted.GraphQL.Types;
using GettingStarted.Mappers;

namespace GettingStarted.GraphQL
{
    public partial class Mutation
    {
        [GraphQLMetadata("editAuthor")]
        public Author EditAuthor(int id, AuthorInput author)
        {
            if (author == null)
            {
                return null;
            }

            var existingAuthor = authorsDataSource.GetAuthor(id);
            if (existingAuthor == null)
            {
                return null;
            }

            existingAuthor.Name = author.Name;
            existingAuthor.Email = author.Email;

            var books = booksDataSource.GetBooksByAuthor(id);

            return AuthorMapper.ConvertModelAuthorToGraphQLAuthor(existingAuthor, books);
        }

        [GraphQLMetadata("newAuthor")]
        public Author NewAuthor(ResolveFieldContext context, AuthorInput author)
        {
            if (author == null)
            {
                return null;
            }

            var newAuthor = AuthorMapper.ConvertAuthorInputToModelAuthor(author);
            newAuthor = authorsDataSource.NewAuthor(newAuthor);

            return AuthorMapper.ConvertModelAuthorToGraphQLAuthor(newAuthor, null);
        }

        [GraphQLMetadata("deleteAuthor")]
        public bool DeleteAuthor(int id)
        {
            return authorsDataSource.DeleteAuthor(id);
        }
    }
}