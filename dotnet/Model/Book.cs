using System.Collections.Generic;

namespace service.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }

        //GraphQL needs empty constructors to instantiate the objects
        public Book()
        {

        }

        public Book(int id, string name, int authorId)
        {
            this.Id = id;
            this.Name = name;
            this.AuthorId = authorId;
        }
    }
}