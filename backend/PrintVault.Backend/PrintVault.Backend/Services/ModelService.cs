using Microsoft.EntityFrameworkCore;
using PrintVault.Backend.Data;
using PrintVault.Backend.DTOs;
using PrintVault.Backend.DTOs.Model.Response;
using PrintVault.Backend.Helpers;
using PrintVault.Backend.Interfaces;

namespace PrintVault.Backend.Services;

public class ModelService : IModelService
{
    private readonly PrintVaultContext _context;
    public ModelService(PrintVaultContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponseDto<List<ModelResponseDto>>> GetModels()
    {
        var models = await _context.PrintModels
            .Select(m => new ModelResponseDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                CreationDate = m.CreationDate,
                ProfileTitle = m.ProfileTitle,
                ProfileDescription = m.ProfileDescription,
                TotalFilamentUsedG = m.TotalFilamentUsedG,
                TotalPrintTimeSeconds = m.TotalPrintTimeSeconds,
                NumberOfPlates = m.NumberOfPlates,
                IsFavorite = m.IsFavorite,
                ImportedAt = m.ImportedAt
            })
            .ToListAsync();

        return ServiceResponseHelper.CreateSuccessResponse(models);
    }
}