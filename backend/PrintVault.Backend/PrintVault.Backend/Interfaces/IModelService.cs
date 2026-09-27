using PrintVault.Backend.DTOs;
using PrintVault.Backend.DTOs.Model.Request;

namespace PrintVault.Backend.Interfaces;

using PrintVault.Backend.DTOs.Model.Response;

public interface IModelService
{
    Task<ServiceResponseDto<List<ModelResponseDto>>> GetModels();
    Task<ServiceResponseDto<ModelResponseDto>> GetModelById(int id);
    Task<ServiceResponseDto<EmptyDto>> DeleteModel(int id);
    Task<ServiceResponseDto<ModelResponseDto>> UpdateModel(int id, UpdateModelDto updateModel);
}