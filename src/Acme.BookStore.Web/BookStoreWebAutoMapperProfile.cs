﻿using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using AutoMapper;

namespace Acme.BookStore.Web;

public class BookStoreWebAutoMapperProfile : Profile
{
    public BookStoreWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<BookDto, CreateUpdateBookDto>();

        CreateMap<CreateUpdateAuthorDto,
                    CreateAuthorDto>();
                    
        CreateMap<AuthorDto, CreateUpdateAuthorDto>();
        CreateMap<CreateUpdateAuthorDto, UpdateAuthorDto>();

        CreateMap<Pages.Books.CreateModalModel.CreateBookViewModel, CreateUpdateBookDto>();

        CreateMap<Pages.Books.EditModalModel.EditBookViewModel, CreateUpdateBookDto>();
        CreateMap<BookDto, Pages.Books.EditModalModel.EditBookViewModel>();
    }
}
