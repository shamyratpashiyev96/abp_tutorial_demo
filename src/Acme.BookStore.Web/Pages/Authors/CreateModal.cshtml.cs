using System;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Authors
{
    public class CreateModalModel : BookStorePageModel
    {
        [BindProperty]
        public CreateUpdateAuthorDto author { get; set; }

        private readonly IAuthorAppService authorAppService;

        public CreateModalModel(IAuthorAppService _authorAppService)
        {
            authorAppService = _authorAppService;
        }

        public void OnGet()
        {
            author = new CreateUpdateAuthorDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateUpdateAuthorDto, CreateAuthorDto>(author);
            await authorAppService.CreateAsync(dto);
            return NoContent();
        }
    }
}