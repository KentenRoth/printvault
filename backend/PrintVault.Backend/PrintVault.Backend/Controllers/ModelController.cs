using Microsoft.AspNetCore.Mvc;
using PrintVault.Backend.Interfaces;

namespace PrintVault.Backend.Controllers;

[ApiController]
[Route("api/model")]
public class ModelController : ControllerBase
{
    private readonly IModelService _modelService;
    
    public ModelController(IModelService modelService)
    {
        _modelService = modelService;
    }

    [HttpGet("allmodels")]
    public async Task<IActionResult> GetModels()
    {
        var models = await _modelService.GetModels();
        
        return Ok(models);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetModelById(int id)
    {
        var model = await _modelService.GetModelById(id);

        if (!model.Success)
        {
            return NotFound();
        }
        return Ok(model);
    }
}