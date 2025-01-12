using MedifyAPI.Core.Models;
using MedifyAPI.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedifyAPI.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HospitalsController : ControllerBase
{
    private readonly IHospitalService _hospitalService;

    public HospitalsController(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Hospital>>> GetAll()
    {
        var hospitals = await _hospitalService.GetAllAsync();
        return Ok(hospitals);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Hospital>> GetById(Guid id)
    {
        var hospital = await _hospitalService.GetByIdAsync(id);
        if (hospital == null)
        {
            return NotFound();
        }
        return Ok(hospital);
    }

    [HttpPost]
    public async Task<ActionResult<Hospital>> Add([FromBody] Hospital hospital)
    {
        var newHospital = await _hospitalService.AddAsync(hospital);
        return CreatedAtAction(nameof(GetById), new { id = newHospital.Id }, newHospital);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Hospital>> Update(Guid id, [FromBody] Hospital hospital)
    {
        if (id != hospital.Id)
        {
            return BadRequest();
        }

        var updatedHospital = await _hospitalService.UpdateAsync(hospital);
        return Ok(updatedHospital);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _hospitalService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
