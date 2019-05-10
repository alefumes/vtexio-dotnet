using System.Collections.Generic;
using GettingStarted.Model;
using GraphQL;
using GraphQL.Types;

namespace GettingStarted.GraphQL.Types
{
    [GraphQLMetadata("BookInputType")]
    public class BookInputType : InputObjectGraphType<Book>
    {
        public BookInputType()
        {
            Name = "BookInput";
            Field(x => x.Name);
            Field(x => x.AuthorId);
        }
    }
}