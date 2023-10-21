using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Books.Seeders
{
    public class BookSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> repo;

        private readonly IAuthorRepository authorRepo;

        private readonly AuthorManager authorManager;

        public BookSeederContributor(
            IRepository<Book, Guid> _repo,
            IAuthorRepository _authorRepo,
            AuthorManager _authorManager)
        {
            this.repo = _repo;
            this.authorRepo = _authorRepo;
            this.authorManager = _authorManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await AuthorSeeder();
            await BookSeeder();

        }

        public async Task BookSeeder()
        {
            List<Author> authors = await authorRepo.GetListAsync();
            List<Book> books = new(){
                new Book{
                    Name = "1984",
                    Type = BookType.Distopia,
                    PublishDate = new DateTime(1949,6,8),
                    Price = 19.84f,
                    AuthorId = authors[0].Id
                },
                new Book{
                    Name = "The Hitchhiker's Guide to the Galaxy",
                    Type = BookType.ScienceFiction,
                    PublishDate = new DateTime(1995,9,27),
                    Price = 42.0f,
                    AuthorId = authors[1].Id
                },
            };
            if(await repo.GetCountAsync() <= 0)
            {
                await repo.InsertManyAsync(books,autoSave:true);
            }
        }

        public async Task AuthorSeeder()
        {
            List<Author> authors = new(){
                await authorManager.CreateAsync(
                    "George Orwell",
                    new DateTime(1903, 06, 25),
                    "Orwell produced literary criticism and poetry, fiction and polemical journalism; and is best known for the allegorical novella Animal Farm (1945) and the dystopian novel Nineteen Eighty-Four (1949)."
                ),
                await authorManager.CreateAsync(
                    "Douglas Adams",
                    new DateTime(1952, 03, 11),
                    "Douglas Adams was an English author, screenwriter, essayist, humorist, satirist and dramatist. Adams was an advocate for environmentalism and conservation, a lover of fast cars, technological innovation and the Apple Macintosh, and a self-proclaimed 'radical atheist'."
                )
            };

            if(await authorRepo.GetCountAsync() <= 0)
            {
                await authorRepo.InsertManyAsync(authors,autoSave:true);
            }
        }
    }
}