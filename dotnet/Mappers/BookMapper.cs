using GettingStarted.GraphQL.Types;

namespace GettingStarted.Mappers
{
    public static class BookMapper
    {
        public static Book ConvertModelBookToGraphQLBook(Model.Book modelBook, Model.Author modelAuthor)
        {
            var graphqlBook = new Book() 
            {
                Id = modelBook.Id,
                Name = modelBook.Name
            };

            if (modelAuthor != null)
            {
                graphqlBook.Author = AuthorMapper.ConvertModelAuthorToGraphQLAuthor(modelAuthor, null);
            }

            return graphqlBook;
        }

        public static Model.Book ConvertGraphQLBookToModelBook(Book graphqlBook)
        {
            int authorId = graphqlBook.Author?.Id ?? -1;
            return new Model.Book(graphqlBook.Id, graphqlBook.Name, authorId);
        }
    }
}