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

public class ModelService : IModelService
{
    private readonly PrintVaultContext _context;
    private readonly IMapper _mapper;

    public ModelService(PrintVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponseDto<List<ModelResponseDto>>> GetModels()
    {
        var models = await _context.PrintModels
            .ProjectTo<ModelResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return ServiceResponseHelper.CreateSuccessResponse(models);
    }

    public async Task<ServiceResponseDto<ModelResponseDto>> GetModelById(int id)
    {
        var model = await _context.PrintModels
            .Where(r => r.Id == id)
            .ProjectTo<ModelResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (model == null)
        {
            return ServiceResponseHelper.CreateErrorResponse<ModelResponseDto>("Model Not Found");
        }

        return ServiceResponseHelper.CreateSuccessResponse(model);
    }

    public async Task<ServiceResponseDto<EmptyDto>> DeleteModel(int id)
    {
        var model = await _context.PrintModels.FirstOrDefaultAsync(r => r.Id == id);

        if (model == null) return ServiceResponseHelper.CreateErrorResponse<EmptyDto>("Model Not Found");
        
        _context.PrintModels.Remove(model);
        await _context.SaveChangesAsync();
        
        return ServiceResponseHelper.CreateSuccessResponse(new EmptyDto());
    }

    public async Task<ServiceResponseDto<ModelResponseDto>> UpdateModel(int id, UpdateModelDto dto)
    {
        var model = await _context.PrintModels
            .Include(r => r.ModelTags)
            .Where(r => r.Id == id)
            .FirstOrDefaultAsync();

        if (model == null) return ServiceResponseHelper.CreateErrorResponse<ModelResponseDto>("Model Not Found");

        if (dto.Title != null)
        {
            if (string.IsNullOrWhiteSpace(dto.Title)) return ServiceResponseHelper.CreateErrorResponse<ModelResponseDto>("Title is Empty");
            model.Title = dto.Title;
        }

        if (dto.Description != null)
        {
            if (string.IsNullOrWhiteSpace(dto.Description)) return ServiceResponseHelper.CreateErrorResponse<ModelResponseDto>("Description is Empty");
            model.Description = dto.Description;
        }

        if (dto.IsFavorite != null)
        {
            model.IsFavorite = dto.IsFavorite.Value;
        }
        
        if (dto.CategoryId != null && dto.RemoveCategory == true) return  ServiceResponseHelper.CreateErrorResponse<ModelResponseDto>("Category Cannot be set and removed.");

        if (dto.RemoveCategory == true)
        {
            model.CategoryId = null;
        }

        if (dto.CategoryId != null)
        {
            var categoryExists = await _context.Categories.AnyAsync(r => r.Id == dto.CategoryId);
            if (!categoryExists) return ServiceResponseHelper.CreateErrorResponse<ModelResponseDto>("Category Not Found");
            
            model.CategoryId = dto.CategoryId;
        }

        if (dto.TagIds != null)
        {
            var newTagIds = dto.TagIds.Distinct().ToList();

            var existingTagCount = await _context.Tags.CountAsync(r => newTagIds.Contains(r.Id));
            if (existingTagCount != newTagIds.Count) return ServiceResponseHelper.CreateErrorResponse<ModelResponseDto>("One or more tags not found");

            var tagsToRemove = model.ModelTags
                .Where(mt => !newTagIds.Contains(mt.TagId))
                .ToList();

            foreach (var modelTag in tagsToRemove)
            {
                model.ModelTags.Remove(modelTag);
            }

            var currentTagIds = model.ModelTags.Select(mt => mt.TagId).ToList();

            foreach (var tagId in newTagIds)
            {
                if (!currentTagIds.Contains(tagId))
                {
                    model.ModelTags.Add(new ModelTag { TagId = tagId });
                }
            }
        }
        
        await _context.SaveChangesAsync();
        return await GetModelById(id);

    }
}