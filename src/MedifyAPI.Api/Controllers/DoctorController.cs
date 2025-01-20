using MedifyAPI.Core.Models;
using MedifyAPI.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedifyAPI.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetAll()
    {
        var doctors = await _doctorService.GetAllAsync();
        return Ok(doctors);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetById(Guid id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }
        return Ok(doctor);
    }

    [HttpPost]
    public async Task<ActionResult<Doctor>> Add([FromBody] Doctor doctor)
    {
        var newDoctor = await _doctorService.AddAsync(doctor);
        return CreatedAtAction(nameof(GetById), new { id = newDoctor.Id }, newDoctor);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Doctor>> Update(Guid id, [FromBody] Doctor doctor)
    {
        if (id != doctor.Id)
        {
            return BadRequest();
        }

        var updatedDoctor = await _doctorService.UpdateAsync(doctor);
        return Ok(updatedDoctor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _doctorService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
