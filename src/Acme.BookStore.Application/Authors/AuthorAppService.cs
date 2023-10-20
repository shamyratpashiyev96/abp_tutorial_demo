using System.Reflection.Metadata.Ecma335;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Authors
{
    public class AuthorAppService : BookStoreAppService, IAuthorAppService
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly AuthorManager _authorManager;

        public AuthorAppService(IAuthorRepository authorRepo, AuthorManager authorManager)
        {
            _authorRepo = authorRepo;
            _authorManager = authorManager;
        }

        public async Task<AuthorDto> GetAsync(Guid id)
        {
            var author = await _authorRepo.GetAsync(id);
            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        public async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input)
        {
            if(input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Author.Name);
            }

            var authors = await _authorRepo.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _authorRepo.CountAsync()
                : await _authorRepo.CountAsync(
                    author => author.Name.Contains(input.Filter)
                    );
            return new PagedResultDto<AuthorDto>(
                totalCount,
                ObjectMapper.Map<List<Author>, List<AuthorDto>>(authors)
            );
        }

        [Authorize(BookStorePermissions.Authors.Create)]
        public async Task<AuthorDto> CreateAsync(CreateAuthorDto input)
        {
            var author = await _authorManager.CreateAsync(
                input.Name,
                input.BirthDate,
                input.ShortBio
            );

            await _authorRepo.InsertAsync(author);

            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        [Authorize(BookStorePermissions.Authors.Edit)]
        public async Task UpdateAsync(Guid id, UpdateAuthorDto input)
        {
            var author = await _authorRepo.GetAsync(id);

            if(author.Name != input.Name)
            {
                await _authorManager.ChangeNameAsync(author, input.Name);
            }

            author.BirthDate = input.BirthDate;
            author.ShortBio = input.ShortBio;

            await _authorRepo.UpdateAsync(author);

        }

        [Authorize(BookStorePermissions.Authors.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _authorRepo.DeleteAsync(id);
        }
    }
}