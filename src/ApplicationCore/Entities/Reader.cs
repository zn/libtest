using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BookReader> Books { get; set; }
    }
}