using AutoMapper;
using PrintVault.Backend.DTOs.Model.Response;
using PrintVault.Backend.Models;

namespace PrintVault.Backend.Mappings;

public class ModelProfile : Profile
{
    public ModelProfile()
    {
        CreateMap<PrintModel, ModelResponseDto>();
    }
}
