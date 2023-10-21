using System;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using Microsoft.AspNetCore.Mvc;

namespace Acme.BookStore.Web.Pages.Authors
{
    public class EditModalModel : BookStorePageModel
    {
        
        [BindProperty]
        public CreateUpdateAuthorDto author { get; set; }

        private readonly IAuthorAppService authorAppService;

        public EditModalModel(IAuthorAppService _authorAppService)
        {
            authorAppService = _authorAppService;
        }

        public async Task OnGetAsync(Guid Id)
        {
            var authorDto = await authorAppService.GetAsync(Id);
            author = ObjectMapper.Map<AuthorDto, CreateUpdateAuthorDto>(authorDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await authorAppService.UpdateAsync(
                author.Id,
                ObjectMapper.Map<CreateUpdateAuthorDto, UpdateAuthorDto>(author)
            );

            return NoContent();
        }
    }
}