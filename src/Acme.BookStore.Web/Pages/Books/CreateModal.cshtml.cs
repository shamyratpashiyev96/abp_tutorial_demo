using System.Dynamic;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Acme.BookStore.Web.Pages.Books
{
    public class CreateModalModel : BookStorePageModel
    {
        private readonly IBookAppService _bookAppService;

        [BindProperty]      //Binds the properties to the <input> tags automatically
        public CreateUpdateBookDto Book { get; set; }
        public CreateModalModel(IBookAppService bookAppService)
        {
            this._bookAppService = bookAppService;
        }

        public void OnGet()
        {
            Book = new CreateUpdateBookDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.CreateAsync(Book);
            return NoContent();
        }
    }
}