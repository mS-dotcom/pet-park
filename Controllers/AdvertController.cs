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
public class AdvertController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<AdvertController> _logger;
    Models.Context _db = new Models.Context();

    public AdvertController(ILogger<AdvertController> logger,IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    [HttpPost("AddAdvert")]
    public async Task<IActionResult> AddAdvert(Advert advert)
    {
        try
        {
                advert.LastDate = DateTime.Now.AddDays(15);       
                _db.Adverts.Add(advert);
                _db.SaveChanges();
            
            
            return Ok(advert);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "AddAdvert";
            log.UserId = advert.UserId;
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }   
    }
    //ADDADVERTPHOTO
    [HttpPost("AddAdvertPhoto")]
    public async Task<IActionResult> AddAdvertPhoto([FromForm]AddAdvertPhotoVM vm)
    {


        try
        {
            if (vm.file != null)
            {
                // Örnek bir dosya kaydetme kodu
                Random rnd = new Random();
                string number = rnd.Next(1, 100).ToString();
                var advert = _db.Adverts.FirstOrDefault(x => x.AdvertId == vm.AdvertId);


                var uniqueFileName = advert.UserId + "_" + number + "_" + vm.file.FileName;
                string path = Path.Combine(_environment.WebRootPath, "AdvertImages");
                string filePath = Path.Combine(path, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.file.CopyToAsync(stream);
                }
                advert.AdvertImageLocation = filePath;
                _db.SaveChanges();
            }
            return Ok();
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "AddAdvertPhoto";
            
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest();
        }
        
    }


    [HttpGet("DeleteAdvert")]
    public IActionResult DeleteAdvert(int advertId)
    {
        try
        {
            var adv = _db.Adverts.FirstOrDefault(x => x.AdvertId == advertId);
            _db.Remove(adv);
            _db.SaveChanges();
            return Ok(adv);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "DeleteAdvert";
            
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }

    [HttpGet("MyAdverts")]
    public IActionResult MyAdverts(int userId)
    {
        try
        {
            var myAds = _db.Adverts.Where(x => x.UserId == userId&&x.LastDate>DateTime.Now).ToList();
            return Ok(myAds);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "Adverts";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }

    [HttpPost("Adverts")]
    public IActionResult Adverts(Search search)
    {
        
        try
        {
            if (search.cityId != 0 && search.cityId != null)
            {

                if (search.districtId != 0 && search.districtId != null)
                {
                    var allAdvertsOnlyCityAndDistrict = _db.Adverts.Where(x => x.LastDate > DateTime.Now).ToList()
                    .Where(x => x.CityId == search.cityId && x.DistrictId == search.districtId)
                    .Skip(((int)search.PageNumber - 1) * (int)search.PageSize)
                    .Take((int)search.PageSize)
                    .ToList();

                    return Ok(allAdvertsOnlyCityAndDistrict);
                }
                else
                {
                    var allAdvertsOnlyCity = _db.Adverts.Where(x => x.LastDate > DateTime.Now).ToList()
                   .Where(x => x.CityId == search.cityId)
                   .Skip(((int)search.PageNumber - 1) * (int)search.PageSize)
                   .Take((int)search.PageSize)
                   .ToList();
                    return Ok(allAdvertsOnlyCity);
                }

            }
            else
            {
                var allAdvertsWithoutLocation = _db.Adverts.Where(x => x.LastDate > DateTime.Now).ToList()
                    .ToList()
                  .Skip(((int)search.PageNumber - 1) * (int)search.PageSize)
                  .Take((int)search.PageSize)
                  .ToList();
                return Ok(allAdvertsWithoutLocation);
            }
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "Adverts";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }
    [HttpGet("MakePrivate")]
    public IActionResult MakePrivate(int AdvertId,int HowManyDays)
    {
        try
        {
            var advert = _db.Adverts.FirstOrDefault(x => x.AdvertId == AdvertId);
            DateTime last = advert.LastDate;
            
            advert.LastDate = last.AddDays(HowManyDays);
            advert.IsPrimary = true;
            _db.SaveChanges();
            return Ok(advert);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "MakePrivate";

            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex.Message);
        }
        
    }
    [HttpGet("AdvertDetail")]
    public IActionResult AdvertDetail(int AdvertId)
    {
        try
        {
            AdvertDetailVM vm = new AdvertDetailVM();
            var advert = _db.Adverts.FirstOrDefault(x => x.AdvertId == AdvertId);
            vm.advert = advert;
            
            if (advert.AnimalInfoShare == true)
            {
                var animal = _db.Animals.FirstOrDefault(x => x.AnimalId == advert.AnimalId);
                vm.animal = animal;
            }
            return Ok(vm);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "AdvertDetail";
            
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }
}

