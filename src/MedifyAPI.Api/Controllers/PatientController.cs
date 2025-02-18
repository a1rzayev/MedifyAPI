using MedifyAPI.Core.Models;
using MedifyAPI.Core.Enums;
using MedifyAPI.Core.DTO;
using MedifyAPI.Core.DTO.Base;
using MedifyAPI.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedifyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetAll()
    {
        var patients = await _patientService.GetAllAsync();
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetById(Guid id)
    {
        var patient = await _patientService.GetByIdAsync(id);
        if (patient == null)
        {
            return NotFound();
        }
        return Ok(patient);
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] Patient patient)
    {
        await _patientService.AddAsync(patient);
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, IUserUpdateDto updateDto)
    {
        var existingUser = await _patientService.GetByIdAsync(id);

        if (existingUser == null)
        {
            return null; // User not found
        }

        // Update user properties
        existingUser.Name = updateDto.Name;
        existingUser.Surname = updateDto.Surname;
        existingUser.Birthdate = updateDto.Birthdate;
        existingUser.Gender = updateDto.Gender;
        existingUser.Phone = updateDto.Phone;
        existingUser.Email = updateDto.Email;
        existingUser.Password = updateDto.Password; // Ideally hashed before saving

        // Save the updated user
        var updatedUser = await _patientService.UpdateAsync(existingUser);


        return Ok();
    }
    [HttpGet("Genders")]
    public async Task<IActionResult> GetGenders()
    {
        var genders = Enum.GetNames(typeof(GenderEnum));
        return Ok(genders);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _patientService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("RendezvouzRequest/{doctorId}/{patientId}")]
    public async Task<IActionResult> RequestRendezvouz(Guid doctorId, Guid patientId, [FromBody] RendezvouzRequestDto requestDto)
    {
        await _patientService.RendezvouzRequestAsync(doctorId, patientId, requestDto.DateTime, requestDto.Description);
        return Ok(new { message = "Rendezvous request submitted successfully" });
    }
}
