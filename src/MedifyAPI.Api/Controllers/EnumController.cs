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
public class EnumController : ControllerBase
{
    private readonly IEnumService _enumService;

    public EnumController(IEnumService enumService)
    {
        _enumService = enumService;
    }

    [HttpGet("Specialities")]
    public async Task<IActionResult> GetSpecialities()
    {
        return Ok(_enumService.GetAllSpecialityEnum());
    }

    [HttpGet("Genders")]
    public async Task<IActionResult> GetGenders()
    {
        return Ok(_enumService.GetAllGenderEnum());
    }

    [HttpGet("RequestStates")]
    public async Task<IActionResult> GetRequestStates()
    {
        return Ok(_enumService.GetAllRequestStateEnum());
    }

}
