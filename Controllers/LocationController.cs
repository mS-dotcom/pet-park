using Microsoft.AspNetCore.Mvc;
using perpark_api.Models;
using perpark_api.Models.Entities;

namespace perpark_api.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    
    private readonly ILogger<UserController> _logger;
    Models.Context _db = new Models.Context();

    public LocationController(ILogger<UserController> logger)
    {
        _logger = logger;
    }


    [HttpPost("AddCity")]
    public IActionResult AddCity(City city)
    {
        try
        {
            _db.Cities.Add(city);
            _db.SaveChanges();
            return Ok(city);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
        
    }
    [HttpPost("AddDistrict")]
    public IActionResult AddDistrict(District district)
    {
        try
        {
            _db.Districts.Add(district);
            _db.SaveChanges();
            return Ok(district);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

    }
    [HttpGet("GetCities")]
    public IActionResult GetCities()
    {
        try
        {
            var cities = _db.Cities.ToList();
            return Ok(cities);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "GetCities";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }

    }
    [HttpPost("GetDistricts")]
    public IActionResult GetDistricts(int CityId)
    {
        try
        {
            var districts = _db.Districts.Where(x => x.CityId == CityId).ToList().OrderByDescending(x=>x.Name).Reverse().ToList();
            return Ok(districts);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "GetDistricts";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }

}

