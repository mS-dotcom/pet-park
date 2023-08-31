using System;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using perpark_api.DevelopmentClasses;
using perpark_api.Models;
using perpark_api.Models.Entities;

namespace perpark_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ComingController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ComingController> _logger;
    Models.Context _db = new Models.Context();

    public ComingController(ILogger<ComingController> logger,IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    [HttpPost("GetComingClinics")]
    public IActionResult GetComingClinics(GetComingVM vm)
    {
        try
        {
            if (vm.DistrictId != 0 && vm.DistrictId != null)
            {
                var model = _db.Clinics.Where(x => x.CityId == vm.CityId && x.DistrictId == vm.DistrictId).ToList();
                return Ok(model);
            }
            else
            {
                var model = _db.Clinics.Where(x => x.CityId == vm.CityId).ToList();
                return Ok(model);
            }
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "GetComingClincs";
            
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }
    [HttpPost("GetComingPetHotels")]
    public IActionResult GetComingPetHotels(GetComingVM vm)
    {
        try
        {
            if (vm.DistrictId != 0 && vm.DistrictId != null)
            {
                var model = _db.PetHotels.Where(x => x.CityId == vm.CityId && x.DistrictId == vm.DistrictId).ToList();
                return Ok(model);
            }
            else
            {
                var model = _db.PetHotels.Where(x => x.CityId == vm.CityId).ToList();
                return Ok(model);
            }
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "GetComingPetHotels";
            
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }
    [HttpPost("GetComingPetWalkers")]
    public IActionResult GetComingPetWalkers(GetComingVM vm)
    {
        try
        {
            if (vm.DistrictId != 0 && vm.DistrictId != null)
            {
                var model = _db.Users.Where(x => x.CityId == vm.CityId && x.DistrictId == vm.DistrictId&&x.UserTypeId==3).ToList();
                return Ok(model);
            }
            else
            {
                var model = _db.Users.Where(x => x.CityId == vm.CityId && x.UserTypeId == 3).ToList();
                return Ok(model);
            }
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "GetComingPetWalkers";
            
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }
}