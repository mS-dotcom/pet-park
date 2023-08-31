using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using perpark_api.Models;
using perpark_api.Models.Entities;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using IronBarCode;
using System;

namespace perpark_api.Controllers;

[ApiController]
[Route("[controller]")]
public class VaccinationController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    
    private readonly ILogger<VaccinationController> _logger;
    Models.Context _db = new Models.Context();

    public VaccinationController(ILogger<VaccinationController> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

 
    [HttpPost("AddVaccine")]
    public async Task<IActionResult> AddVaccine(Vaccine vaccine)
    {
        try
        {
            _db.Vaccines.Add(vaccine);
            _db.SaveChanges();
            return Ok(vaccine);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "Login";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }
    [HttpPost("DeleteVaccine")]
    public async Task<IActionResult> DeleteVaccine(int vaccineId)
    {
        try
        {
            var vac = _db.Vaccines.FirstOrDefault(x => x.VaccineId == vaccineId);
            _db.Vaccines.Remove(vac);
            _db.SaveChanges();
            return Ok(vac);
        }
        catch (Exception ex)
        {

            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "Login";
            _db.Logs.Add(log);
            _db.SaveChanges();

            return BadRequest(ex);
        }
        
    }
}


