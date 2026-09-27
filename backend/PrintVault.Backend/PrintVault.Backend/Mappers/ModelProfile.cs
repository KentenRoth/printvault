using AutoMapper;
using PrintVault.Backend.DTOs.Model.Response;
using PrintVault.Backend.Models;

namespace PrintVault.Backend.Mappings;

public class ModelProfile : Profile
{
    public ModelProfile()
    {
        CreateMap<PrintModel, ModelResponseDto>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.ModelTags.Select(mt => mt.Tag)));
        CreateMap<Plate, PlateResponseDto>();
        CreateMap<Tag, TagResponseDto>();
        CreateMap<Category, CategoryResponseDto>();
    }
}
