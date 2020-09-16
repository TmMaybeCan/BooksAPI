using System;
using System.Collections.Generic;

namespace BooksAPI.Models
{
    public partial class Books
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }

        public virtual Authors Author { get; set; }
    }
}

















