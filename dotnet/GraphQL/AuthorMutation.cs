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
        public Author EditAuthor(int id, AuthorInput authorInput)
        {
            if (authorInput == null)
            {
                return null;
            }

            var existingAuthor = authorsDataSource.GetAuthor(id);
            if (existingAuthor == null)
            {
                return null;
            }

            existingAuthor.Name = authorInput.Name;
            existingAuthor.Email = authorInput.Email;

            var books = booksDataSource.GetBooksByAuthor(id);
            
            return AuthorMapper.ConvertModelAuthorToGraphQLAuthor(existingAuthor, books);
        }

        [GraphQLMetadata("newAuthor")]
        public Author NewAuthor(ResolveFieldContext context, AuthorInput authorInput)
        {
            if (authorInput == null)
            {
                return null;
            }

            var author = AuthorMapper.ConvertAuthorInputToModelAuthor(authorInput);
            author = authorsDataSource.NewAuthor(author);
            
            return AuthorMapper.ConvertModelAuthorToGraphQLAuthor(author, null);
        }

        [GraphQLMetadata("deleteAuthor")]
        public bool DeleteAuthor(int id)
        {
            return authorsDataSource.DeleteAuthor(id);
        }
    }
}