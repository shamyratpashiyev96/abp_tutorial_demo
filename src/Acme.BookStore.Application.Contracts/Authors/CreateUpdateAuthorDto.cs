using System;
using System.ComponentModel.DataAnnotations;
// using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form; //This is available only in Web layer
namespace Acme.BookStore.Authors
{
    public class CreateUpdateAuthorDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(AuthorConsts.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        // [TextArea] //It makes the input tag textarea but it is available only in Web layer   
        public string ShortBio { get; set; }
    }
}