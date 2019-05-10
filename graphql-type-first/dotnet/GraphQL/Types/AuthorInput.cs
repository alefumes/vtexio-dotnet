using System.Collections.Generic;
using GettingStarted.Model;
using GraphQL;
using GraphQL.Types;

namespace GettingStarted.GraphQL.Types
{
    [GraphQLMetadata("AuthorInputType")]
    public class AuthorInputType : InputObjectGraphType<Author>
    {
        public AuthorInputType()
        {
            Name = "AuthorInput";
            Field(x => x.Name);
            Field(x => x.Email);
        }
    }
}