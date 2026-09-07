using PrintVault.Backend.DTOs;
using PrintVault.Backend.DTOs.Model.Request;
using PrintVault.Backend.DTOs.Model.Response;

namespace PrintVault.Backend.Interfaces;

public interface ICategoryService
{
    Task<ServiceResponseDto<List<CategoryResponseDto>>> GetAllCategories();
    Task<ServiceResponseDto<CategoryResponseDto>> GetCategoryById(int id);
    Task<ServiceResponseDto<CategoryResponseDto>> CreateCategory(CreateCategoryDto dto);
    
}