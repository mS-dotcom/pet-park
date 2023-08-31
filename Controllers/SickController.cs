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
public class SickController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    
    private readonly ILogger<SickController> _logger;
    Models.Context _db = new Models.Context();

    public SickController(ILogger<SickController> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

 
    [HttpPost("AddSick")]
    public async Task<IActionResult> AddSick(Sickness sick)
    {
        try
        {
            _db.Sickess.Add(sick);
            _db.SaveChanges();
            return Ok(sick);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "AddSick";
            
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
     
    }
    [HttpPost("DeleteSick")]
    public async Task<IActionResult> DeleteSick(int sicknessId)
    {
       
        try
        {
            var sck = _db.Sickess.FirstOrDefault(x => x.SicknessId == sicknessId);
            _db.Sickess.Remove(sck);
            _db.SaveChanges();
            return Ok(sck);

        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "DeleteSick";

            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }
}


