using System;
using System.Dynamic;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Acme.BookStore.Web.Pages.Books
{
    public class CreateModalModel : BookStorePageModel
    {
        private readonly IBookAppService _bookAppService;
        public List<SelectListItem> authors { get; set; }

        [BindProperty]      //Binds the properties to the <input> tags automatically
        public CreateBookViewModel Book { get; set; }
        public CreateModalModel(IBookAppService bookAppService)
        {
            this._bookAppService = bookAppService;
        }

        public async Task OnGetAsync()
        {
            Book = new CreateBookViewModel();

            var authorLookup = await _bookAppService.GetAuthorLookupAsync();

            authors = authorLookup.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.CreateAsync(
                ObjectMapper.Map<CreateBookViewModel, CreateUpdateBookDto>(Book)
                );
            return NoContent();
        }

        public class CreateBookViewModel
        {
            [SelectItems(nameof(authors))]
            [DisplayName("Author")]
            public Guid AuthorId { get; set; }

            [Required]
            [StringLength(128)]
            public string Name { get; set; }

            [Required]
            public BookType Type { get; set; } = BookType.Undefined;

            [Required]
            [DataType(DataType.Date)]
            public DateTime PublishDate { get; set; } = DateTime.Now;

            [Required]
            public float Price { get; set; }
        }
    }
}