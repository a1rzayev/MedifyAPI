using MedifyAPI.Core.Models;
using MedifyAPI.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedifyAPI.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly ILogService _logService;

    public LogsController(ILogService logService)
    {
        _logService = logService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Log>>> GetAll()
    {
        var logs = await _logService.GetAllAsync();
        return Ok(logs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Log>> GetById(Guid id)
    {
        var log = await _logService.GetByIdAsync(id);
        if (log == null)
        {
            return NotFound();
        }
        return Ok(log);
    }

    [HttpPost]
    public async Task<ActionResult<Log>> Add([FromBody] Log log)
    {
        var newLog = await _logService.AddAsync(log);
        return CreatedAtAction(nameof(GetById), new { id = newLog.Id }, newLog);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Log>> Update(Guid id, [FromBody] Log log)
    {
        if (id != log.Id)
        {
            return BadRequest();
        }

        var updatedLog = await _logService.UpdateAsync(log);
        return Ok(updatedLog);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _logService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}