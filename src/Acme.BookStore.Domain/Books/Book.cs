using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Books
{
    public class Book : AuditedAggregateRoot<Guid>
    {
        [Required]
        public string Name { get; set; }

        public BookType Type { get; set; }
        
        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}