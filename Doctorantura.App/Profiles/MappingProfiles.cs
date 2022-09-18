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
            CreateMap<ColumnNum, ColumnNumCreateDto>().ReverseMap();
            CreateMap<ColumnNum, ColumnNumUpdateDto>().ReverseMap();

            CreateMap<LineNum, LineNumCreateDto>().ReverseMap();
            CreateMap<LineNum, LineNumUpdateDto>().ReverseMap();
        }
    }
}
