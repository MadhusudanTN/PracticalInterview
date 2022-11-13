using Ajmera.Dtos;
using Ajmera.Models;
using AutoMapper;

namespace Ajmera.Helper
{
    public class AjemraMapper : Profile
    {
        public AjemraMapper()
        {
            CreateMap<Book, BookDto>()
                .ForMember(destinationMember: dest => dest.Name, memberOptions: opt => opt.MapFrom(src => src.BookName))
                .ReverseMap();
        }
    }
}