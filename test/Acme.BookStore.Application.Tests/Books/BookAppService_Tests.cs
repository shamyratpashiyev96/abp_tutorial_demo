using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace Acme.BookStore.Books
{
    public class BookAppService_Tests : BookStoreApplicationTestBase
    {
        private readonly IBookAppService _bookAppService;
        private readonly IAuthorAppService _authorAppService;

        public BookAppService_Tests()
        {
            this._bookAppService = GetRequiredService<IBookAppService>();
            this._authorAppService = GetRequiredService<IAuthorAppService>();
        }

        [Fact]
        public async Task Should_Get_List_Of_Books()
        {
            var result = await _bookAppService.GetListAsync(new PagedAndSortedResultRequestDto());
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(item=>item.Name == "1984" && 
                                        item.AuthorName == "George Orwell");
        }

        [Fact]
        public async Task Should_Create_A_Valid_Book()
        {
            var authors = await _authorAppService.GetListAsync(new GetAuthorListDto());
            var firstAuthor = authors.Items.First();

            var result = await _bookAppService.CreateAsync(
                new CreateUpdateBookDto
                {
                    AuthorId = firstAuthor.Id,
                    Name = "New Test Book 42",
                    Price = 10,
                    PublishDate = System.DateTime.Now,
                    Type = BookType.ScienceFiction
                }
            );

            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("New Test Book 42");
        }

        [Fact]
        public async Task Should_Not_Create_A_Book_Without_Name()
        {
            var exception = await Assert.ThrowsAsync<AbpValidationException>(async() =>{
                await _bookAppService.CreateAsync(
                    new CreateUpdateBookDto{
                        Name = "",
                        Price = 10,
                        PublishDate = System.DateTime.Now,
                        Type = BookType.ScienceFiction
                    }
                );
            });

            exception.ValidationErrors.ShouldContain(err => err.MemberNames.Any(mem=>mem == "Name"));
        }
    }
}