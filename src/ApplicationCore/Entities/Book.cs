using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime? PublishDate { get; set; }
        public string PosterUrl { get; set; }
        public int TotalAmount { get; set; }
        public int Available { get; set; }

        public ICollection<BookReader> Readers { get; set; }
    }
}