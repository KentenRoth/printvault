using Microsoft.AspNetCore.Mvc;
using PrintVault.Backend.Interfaces;

namespace PrintVault.Backend.Controllers;

[ApiController]
[Route("api/tag")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;
    
    public  TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet("alltags")]
    public async Task<IActionResult> GetTags()
    {
        var tags = await _tagService.GetTags();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTagById(int id)
    {
        var tag = await _tagService.GetTagById(id);
        return Ok(tag);
    }
}