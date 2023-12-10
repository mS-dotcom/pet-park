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
public class ClinicController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ClinicController> _logger;
    Models.Context _db = new Models.Context();

    public ClinicController(ILogger<ClinicController> logger,IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    [HttpGet("Clinics")]
    public IActionResult Clinics(Search search)
    {
        try
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogType = "ClinicCounter";
            _db.Logs.Add(log);
            _db.SaveChanges();
            if (search.cityId != 0 && search.cityId != null)
            {

                if (search.districtId != 0 && search.districtId != null)
                {
                    var allClinicsOnlyCityAndDistrict = _db.Clinics
                    .Where(x => x.CityId == search.cityId && x.DistrictId == search.districtId && x.IsApproved == true)
                    .Skip(((int)search.PageNumber - 1) * (int)search.PageSize)
                    .Take((int)search.PageSize)
                    .ToList();

                    return Ok(allClinicsOnlyCityAndDistrict);
                }
                else
                {
                    var allClinicsOnlyCity = _db.Clinics
                   .Where(x => x.CityId == search.cityId && x.IsApproved == true)
                   .Skip(((int)search.PageNumber - 1) * (int)search.PageSize)
                   .Take((int)search.PageSize)
                   .ToList();
                    return Ok(allClinicsOnlyCity);
                }

            }
            else
            {
                var allClinisWithoutLocation = _db.Clinics
                  .Where(x => x.IsApproved == true&&x.LastDate>DateTime.Now)
                  .Skip(((int)search.PageNumber - 1) * (int)search.PageSize)
                  .Take((int)search.PageSize)
                  .ToList();
                return Ok(allClinisWithoutLocation);
            }
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "Clinics";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }
    [HttpGet("GetAllClinics")]
    public IActionResult GetAllClinics()
    {
        var allClinics = _db.Clinics.Where(x => x.IsApproved == true && x.Lat != null && x.Lat != "").ToList();
        return Ok(allClinics);
    }
    [HttpGet("ClinicDetail")]
    public IActionResult ClinicDetail(int clinicId)
    {
        try
        {
            var clinic = _db.Clinics.FirstOrDefault(x => x.ClinicId== clinicId&&x.IsApproved==true);
            return Ok(clinic);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "ClinicDetail";
           
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }
    [HttpPost("AddClinic")]
    public IActionResult AddClinic(Clinic clinic)
    {
        try
        {
            var vet = _db.Veterinary.FirstOrDefault(x => x.VeteniaryId == clinic.VeterinaryId);
            if (vet.HasPayment == true)
            {
                clinic.IsApproved = true;
            }
            clinic.LastDate = vet.LastDate;
            _db.Add(clinic);
            vet.HasClinicPinned = true;
            _db.SaveChanges();
            return Ok(clinic);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "AddClinic";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }
}

