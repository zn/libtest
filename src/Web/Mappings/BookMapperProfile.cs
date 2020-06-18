using ApplicationCore;
using ApplicationCore.Entities;
using AutoMapper;
using System;
using Web.ViewModels;

namespace Web.Mappings
{
    public class BookMapperProfile : Profile
    {
        public BookMapperProfile()
        {
            CreateMap<Book, BookViewModel>()
                .ReverseMap();
        }
    }
}