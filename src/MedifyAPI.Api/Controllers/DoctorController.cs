using MedifyAPI.Core.Models;
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

        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Diplomas");

        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        var fileExtension = Path.GetExtension(pdf.FileName);
        var newFileName = $"{id}{fileExtension}";
        var filePath = Path.Combine(uploadPath, newFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await pdf.CopyToAsync(fileStream);
        }

        return Ok(new { message = "File uploaded successfully", filePath });
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
    public IActionResult GetAllDiplomas()
    {
        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Diplomas");

        if (!Directory.Exists(uploadPath))
        {
            return NotFound("No diplomas found.");
        }

        var files = Directory.GetFiles(uploadPath)
                             .Select(file => new
                             {
                                 DoctorId = Path.GetFileNameWithoutExtension(file),
                                 FileName = Path.GetFileName(file),
                                 FilePath = file
                             })
                             .ToList();

        return Ok(files);
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
        if (id != doctor.Id)
        {
            return BadRequest();
        }
        var doc = _doctorService.UpdateAsync(doctor);

        return NoContent();
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

}
