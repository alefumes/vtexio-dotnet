using System.Collections.Generic;

namespace GettingStarted.GraphQL.Types
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Book> Books { get; set; }
    }
}