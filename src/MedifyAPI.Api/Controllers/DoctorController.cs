using MedifyAPI.Core.Models;
using MedifyAPI.Core.Models.Requests;
using MedifyAPI.Core.Services;
using MedifyAPI.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

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

    [HttpPost("VerifyDegree/{id}")]
    public async Task<IActionResult> VerifyDegree(Guid id, IFormFile pdf)
    {
        if (pdf == null || pdf.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        // Validate file type
        var allowedExtensions = new[] { ".pdf" };
        var fileExtension = Path.GetExtension(pdf.FileName).ToLower();
        if (!allowedExtensions.Contains(fileExtension))
        {
            return BadRequest("Invalid file format. Only PDFs are allowed.");
        }

        // Process verification
        await _doctorService.VerifyDegreeRequestAsync(id);

        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Diplomas");

        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
        var newFileName = $"{id}{fileExtension}";
        var filePath = Path.Combine(uploadPath, newFileName);

        // Save file
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await pdf.CopyToAsync(fileStream);
        }

        return Ok(new { message = "File uploaded successfully", filePath });
    }

    [HttpGet("HasPendingRequest/{id}")]
    public async Task<bool> HasPendingRequest(Guid id)
    {
        return await _doctorService.HasPendingRequestAsync(id);
    }

    [HttpGet("IsValidated/{id}")]
    public async Task<bool> IsValidated(Guid id)
    {
        return await _doctorService.IsValidated(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetById(Guid id)
    {
        var doctor = await _doctorService.GetByIdAsync(id);
        if (doctor == null)
        {
            return NotFound();
        }
        // var workDayHours = JsonConvert.DeserializeObject<List<WorkDayHours>>(doctor.WorkDaysHours);
        // doctor.WorkDaysHours = JsonConvert.SerializeObject(workDayHours); // Re-serialize for consistency

        return Ok(doctor);
    }

    //[Authorize()]
    [HttpGet("Diplomas")]
    public async Task<IEnumerable<VerifyDegreeRequest>> GetAllDiplomas()
    {
        // var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Diplomas");

        // if (!Directory.Exists(uploadPath))
        // {
        //     return NotFound("No diplomas found.");
        // }

        // var files = Directory.GetFiles(uploadPath)
        //                      .Select(file => new
        //                      {
        //                          DoctorId = Path.GetFileNameWithoutExtension(file),
        //                          FileName = Path.GetFileName(file),
        //                          FilePath = file
        //                      })
        //                      .ToList();

        // return Ok(files);
        var verifyRequests = await _doctorService.GetAllVerifyDegreeRequestAsync();
        return verifyRequests;
    }
    [HttpGet("DownloadDiploma/{id}")]
    public IActionResult DownloadDiploma(Guid id)
    {
        try
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Diplomas");

            // Search for a file that matches the Doctor ID
            var files = Directory.GetFiles(uploadPath, $"{id}.pdf");

            if (files.Length == 0)
            {
                return NotFound(new { message = "No diploma found for this doctor." });
            }

            var filePath = files[0]; // Assuming one file per doctor
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var contentType = "application/pdf"; // Assuming PDFs

            var fileName = Path.GetFileName(filePath); // Extract the filename

            return File(fileBytes, contentType, fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error downloading file.", error = ex.Message });
        }
    }


    [HttpPost]
    public async Task<ActionResult<Doctor>> Add([FromBody] Doctor doctor)
    {
        var newDoctor = await _doctorService.AddAsync(doctor);
        return CreatedAtAction(nameof(GetById), new { id = newDoctor.Id }, newDoctor);
    }

    [HttpPut("{id}")]
public async Task<IActionResult> Update(Guid id, [FromBody] Doctor doctor)
{

    var existingDoctor = await _doctorService.GetByIdAsync(id);
    if (existingDoctor == null)
    {
        return NotFound("Doctor not found.");
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

    [HttpGet("Specialities")]
    public async Task<IActionResult> GetSpecialities()
    {
        var specialities = Enum.GetNames(typeof(SpecialityEnum));
        return Ok(specialities);
    }

    [HttpGet("Genders")]
    public async Task<IActionResult> GetGenders()
    {
        var genders = Enum.GetNames(typeof(GenderEnum));
        return Ok(genders);
    }


    [HttpPost("ApproveDegree/{id}")]
    public async Task<IActionResult> ApproveDegree(Guid requestId)
    {
        await _doctorService.ApproveDegreeAsync(requestId);
        return Ok();
    }

    [HttpPost("DenyDegree/{id}")]
    public async Task<IActionResult> DenyDegree(Guid requestId)
    {
        await _doctorService.DenyDegreeAsync(requestId);
        return Ok();
    }
}
