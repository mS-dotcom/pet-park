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
public class VeterinaryController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<VeterinaryController> _logger;
    Models.Context _db = new Models.Context();

    public VeterinaryController(ILogger<VeterinaryController> logger,IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    [HttpGet("CheckStatus")]
    public IActionResult CheckStatus(int userId)
    {
        var vet = _db.Veterinary.FirstOrDefault(x => x.userId == userId);
        if (vet.IsPrimary == true)
        {
            if (vet.LastDate < DateTime.Now)
            {
                vet.IsPrimary = false;
                vet.HasPayment = false;
                vet.HasClinicPinned = false;
                var clinic = _db.Clinics.FirstOrDefault(x => x.VeterinaryId == vet.VeteniaryId);
                _db.Clinics.Remove(clinic);
                _db.SaveChanges();
                return BadRequest("Ödeme Yapılmamış veya Tarihi Geçmiş Lütfen Ödeme Yapınız");
            }
            
            else
            {
                return Ok(vet);
            }
        }
        else
        {
            return BadRequest("Ödeme Yapılmamış veya Onay Bekleme Aşamasındasınız");
        }
        
    }
    [HttpGet("GetVeterinaryAccount")]
    public IActionResult GetVeterinaryAccount(int userId)
    {
        var vet = _db.Veterinary.FirstOrDefault(x => x.userId == userId);
        return Ok(vet);
    }
    [HttpGet("HasPayment")]
    public IActionResult HasPayment(int userId)
    {
        var vet = _db.Veterinary.FirstOrDefault(x => x.userId == userId);
        if (vet.HasPayment == true)
        {
            return Ok(vet);
        }
        else
        {

            return BadRequest("Ödeme Yapılmamış Lütfen Ödeme Yapınız");
        }
    }
    [HttpGet("HasFileSend")]
    public IActionResult HasFileSend(int userId)
    {
        var vet = _db.Veterinary.FirstOrDefault(x => x.userId == userId);
        if (vet.FileLocation!=null&&vet.FileLocation!="")
        {
            return Ok(vet);
        }
        else
        {

            return BadRequest("Dosya ile ilgili bir eksiklik/hata veya onay süreci mevcut.");
        }
    }
    [HttpGet("EnablePayment")]
    public IActionResult EnablePayment(int userId,int dayCount)
    {
        try
        {
            var vet = _db.Veterinary.FirstOrDefault(x => x.userId == userId);
            vet.HasPayment = true;
            vet.LastDate = DateTime.Now.AddDays(dayCount + 15);
            _db.SaveChanges();
            return Ok(vet);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.UserId = userId;
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "EnablePayment";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
        
    }
    [HttpGet("HasClinicPinned")]
    public IActionResult HasClinicPinned(int userId)
    {
        try
        {
            var veterinary = _db.Veterinary.FirstOrDefault(x => x.userId == userId);
            if (veterinary.HasClinicPinned == true)
            {
                var clinic = _db.Clinics.FirstOrDefault(x => x.VeterinaryId == veterinary.VeteniaryId);
                return Ok(clinic);
            }
            else
            {
                return BadRequest("Klinik Henüz Pinlenmemiş");
            }
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.UserId = userId;
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "HasClinicPinned";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }

    [HttpGet("IsErrorUser")]
    public IActionResult IsErrorUser(int userId)
    {
        var veterinary = _db.Veterinary.FirstOrDefault(x => x.userId == userId);
        if (veterinary.HasPayment == false)
        {
            return BadRequest("Ödeme Yapılması Gerekiyor. İletişim için info@pet-park.com.tr ");    
        }
       else if (veterinary.FileLocation ==null||veterinary.FileLocation=="")
        {
            return BadRequest("Gönderilen Belgelerde Sorun Var. İletişim için info@pet-park.com.tr ");
        }
       else if (veterinary.HasClinicPinned == false || veterinary.HasClinicPinned== null)
        {
            return BadRequest("Clinic Pinleme Konusunda Sorun Var. İletişim için info@pet-park.com.tr ");
        }
        else
        {
            return Ok("Onay aşamasındasınız, üç gün içinde profiliniz aktif olmaz info@pet-park.com.tr mail atabilirsiniz.");
        }
    }
}

