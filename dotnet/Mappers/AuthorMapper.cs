using System.Collections.Generic;
using GettingStarted.GraphQL.Types;

namespace GettingStarted.Mappers
{
    public static class AuthorMapper
    {
        public static Author ConvertModelAuthorToGraphQLAuthor(Model.Author modelAuthor, List<Model.Book> modelBooks)
        {
            var graphqlAuthor = new Author() 
            {
                Id = modelAuthor.Id,
                Name = modelAuthor.Name,
                Email = modelAuthor.Email
            };

            if (modelBooks != null)
            {
                graphqlAuthor.Books = new List<Book>();
                foreach (var modelBook in modelBooks)
                {
                    graphqlAuthor.Books.Add(BookMapper.ConvertModelBookToGraphQLBook(modelBook, modelAuthor));
                }
            }

            return graphqlAuthor;
        }

        public static Model.Author ConvertGraphQLAuthorToModelAuthor(Author graphqlAuthor)
        {
            return new Model.Author()
            {
                Id = graphqlAuthor.Id, 
                Name = graphqlAuthor.Name,
                Email = graphqlAuthor.Email
            };
        }

        public static Model.Author ConvertAuthorInputToModelAuthor(AuthorInput authorInput)
        {
            return new Model.Author()
            {
                Name = authorInput.Name,
                Email = authorInput.Email
            };
        }
    }
}