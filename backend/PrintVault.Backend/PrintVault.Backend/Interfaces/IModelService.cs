using PrintVault.Backend.DTOs;

namespace PrintVault.Backend.Interfaces;

using PrintVault.Backend.DTOs.Model.Response;

public interface IModelService
{
    Task<ServiceResponseDto<List<ModelResponseDto>>> GetModels();
    Task<ServiceResponseDto<ModelResponseDto>> GetModelById(int id);
}