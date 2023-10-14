using System;

namespace Acme.BookStore.Books
{
    public class CreateUpdateDto
    {
        public string Name { get; set; }

        public BookType Type { get; set; }
        
        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}