using Application.Dto;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Post, PostDto>().ReverseMap();

        CreateMap<Category, CategoryViewModel>();
        CreateMap<Post, PostViewModel>();
        CreateMap<User, UserViewModel>();
    }
}

