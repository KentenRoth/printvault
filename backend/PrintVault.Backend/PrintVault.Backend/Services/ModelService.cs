using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            .Include(r => r.Plates)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (model == null)
        {
            return ServiceResponseHelper.CreateErrorResponse<ModelResponseDto>("Model Not Found");
        }

        var modelResponse = _mapper.Map<ModelResponseDto>(model);

        return ServiceResponseHelper.CreateSuccessResponse(modelResponse);
    }
}