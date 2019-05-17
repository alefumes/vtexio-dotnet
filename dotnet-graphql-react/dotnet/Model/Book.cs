using System.Collections.Generic;

namespace GettingStarted.Model
{
    public class Book
    {
        public int Id { get; set; }
        public int CacheId { get; set; }
        public string Name { get; set; }
        public string[] Authors { get; set; }
    }
}