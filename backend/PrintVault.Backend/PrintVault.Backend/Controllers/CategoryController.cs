using Microsoft.AspNetCore.Mvc;
using PrintVault.Backend.DTOs.Model.Request;
using PrintVault.Backend.Interfaces;
using PrintVault.Backend.Models;

namespace PrintVault.Backend.Controllers;

[ApiController]
[Route("api/category")]

public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("allCategories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var category = await _categoryService.GetAllCategories();
        return Ok(category);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await _categoryService.GetCategoryById(id);
        return Ok(category);
    }

    [HttpPost("categories")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
    {
        var category = await _categoryService.CreateCategory(dto);
        return Ok(category);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _categoryService.DeleteCategory(id);
        return Ok(category);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CreateCategoryDto dto)
    {
        var category = await _categoryService.UpdateCategory(id, dto);
        return Ok(category);
    }
}