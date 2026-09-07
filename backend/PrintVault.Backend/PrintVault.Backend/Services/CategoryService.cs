using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PrintVault.Backend.Data;
using PrintVault.Backend.DTOs;
using PrintVault.Backend.DTOs.Model.Request;
using PrintVault.Backend.DTOs.Model.Response;
using PrintVault.Backend.Helpers;
using PrintVault.Backend.Interfaces;
using PrintVault.Backend.Models;

namespace PrintVault.Backend.Services;

public class CategoryService : ICategoryService
{
    private readonly PrintVaultContext _context;
    private readonly IMapper _mapper;
    
    public CategoryService(PrintVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponseDto<List<CategoryResponseDto>>> GetAllCategories()
    {
        var categories = await _context.Categories
            .ProjectTo<CategoryResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        return ServiceResponseHelper.CreateSuccessResponse(categories);
    }

    public async Task<ServiceResponseDto<CategoryResponseDto>> GetCategoryById(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(r => r.Id == id);

        if (category == null) ServiceResponseHelper.CreateErrorResponse<CategoryResponseDto>("Category Not Found");
        
        var categoryResponse = _mapper.Map<CategoryResponseDto>(category);
        return ServiceResponseHelper.CreateSuccessResponse(categoryResponse);
    }

    public async Task<ServiceResponseDto<CategoryResponseDto>> CreateCategory(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name
        };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        
        var responseDto = _mapper.Map<CategoryResponseDto>(category);
        return ServiceResponseHelper.CreateSuccessResponse(responseDto);
    }
    
}