using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PrintVault.Backend.Data;
using PrintVault.Backend.DTOs;
using PrintVault.Backend.DTOs.Model.Response;
using PrintVault.Backend.Helpers;
using PrintVault.Backend.Interfaces;

namespace PrintVault.Backend.Services;

public class TagService : ITagService
{
    private readonly PrintVaultContext _context;
    private readonly IMapper _mapper;

    public TagService(PrintVaultContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponseDto<List<TagResponseDto>>> GetTags()
    {
        var tags = await _context.Tags
            .ProjectTo<TagResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return ServiceResponseHelper.CreateSuccessResponse(tags);
    }

    public async Task<ServiceResponseDto<TagResponseDto>> GetTagById(int id)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(r => r.Id == id);
        if (tag == null) ServiceResponseHelper.CreateErrorResponse<TagResponseDto>("Tag Not Found");
        
        var tagResponse = _mapper.Map<TagResponseDto>(tag);
        return ServiceResponseHelper.CreateSuccessResponse<TagResponseDto>(tagResponse);
    }
}