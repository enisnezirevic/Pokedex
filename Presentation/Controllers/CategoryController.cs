﻿using Application.Services.Category.Command;
using Application.Services.Category.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ISender _sender;

    public CategoryController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetCategoryListQueryAsync()
    {
        var result = await _sender.Send(new GetCategoryListQuery());
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoryQueryAsync([FromQuery(Name = "name")] string name)
    {
        var result = await _sender.Send(new GetCategoryQuery(name));
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryCommand command)
    {
        var result = await _sender.Send(command);
        return CreatedAtAction("GetCategoryQuery", new { name = result.Name }, result);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchCategoryAsync([FromBody] PatchCategoryCommand command)
    {
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }

        return Ok($"{command.Name.Trim()} was successfully updated to a new value of \"{command.NewValue.Trim()}\".");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategoryAsync([FromBody] DeleteCategoryCommand command)
    {
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }

        return Ok("Category was deleted successfully.");
    }
}