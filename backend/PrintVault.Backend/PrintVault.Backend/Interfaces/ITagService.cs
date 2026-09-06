using PrintVault.Backend.DTOs;
using PrintVault.Backend.DTOs.Model.Response;

namespace PrintVault.Backend.Interfaces;

public interface ITagService
{
    Task<ServiceResponseDto<List<TagResponseDto>>> GetTags();
    Task<ServiceResponseDto<TagResponseDto>> GetTagById(int id);
}