using System.Collections.Generic;

namespace service.Model
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        //GraphQL needs empty constructors to instantiate the objects
        public Author()
        {

        }

        public Author(int id, string name, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
        }
    }
}