using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Books.Seeders
{
    public class BookSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> repo;

        public BookSeederContributor(IRepository<Book, Guid> _repo)
        {
            this.repo = _repo;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            List<Book> books = new(){
                new Book{
                    Name = "1984",
                    Type = BookType.Distopia,
                    PublishDate = new DateTime(1949,6,8),
                    Price = 19.84f
                },
                new Book{
                    Name = "The Hitchhiker's Guide to the Galaxy",
                    Type = BookType.ScienceFiction,
                    PublishDate = new DateTime(1995,9,27),
                    Price = 42.0f
                },
            };

            await repo.InsertManyAsync(books);
        }
    }
}