using ApplicationCore;
using ApplicationCore.Entities;
using AutoMapper;
using System;
using Web.ViewModels;

namespace Web.Mappings
{
    public class ReaderMapperProfile : Profile
    {
        public ReaderMapperProfile()
        {
            CreateMap<Reader, ReaderViewModel>()
                .ReverseMap();
        }
    }
}