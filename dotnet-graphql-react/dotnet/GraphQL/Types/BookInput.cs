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
            Field(x => x.Id, nullable: true);
            Field(x => x.CacheId, nullable: true);
            Field(x => x.Name, nullable: true);
            Field(x => x.Authors);
        }
    }
}