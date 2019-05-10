using System.Collections.Generic;

namespace GettingStarted.GraphQL.Types
{
    public class BookInput
    {
        public string Name { get; set; }
        public int AuthorId { get; set; }
    }
}