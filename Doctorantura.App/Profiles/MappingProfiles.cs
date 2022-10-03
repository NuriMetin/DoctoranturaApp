using AutoMapper;
using Doctorantura.App.Dtos.ColumnNumDtos;
using Doctorantura.App.Dtos.LineNumDtos;
using Doctorantura.App.Models;

namespace Doctorantura.App.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Column, ColumnNumCreateDto>().ReverseMap();
            CreateMap<Column, ColumnNumUpdateDto>().ReverseMap();

            CreateMap<Line, LineNumCreateDto>().ReverseMap();
            CreateMap<Line, LineNumUpdateDto>().ReverseMap();
        }
    }
}
