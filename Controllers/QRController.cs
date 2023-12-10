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
using perpark_api.DevelopmentClasses;

namespace perpark_api.Controllers;

[ApiController]
[Route("[controller]")]
public class QRController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    
    private readonly ILogger<QRController> _logger;
    Models.Context _db = new Models.Context();

    public QRController(ILogger<QRController> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

 
    [HttpPost("GenerateQR")]
    public async Task<IActionResult> GenerateQR([FromBody]Animal animal)
    {
        try
        {
            var findAnimal = _db.Animals.FirstOrDefault(x => x.AnimalId == animal.AnimalId && x.UserId == animal.UserId && x.Name == animal.Name);
            if (String.IsNullOrEmpty(findAnimal.QRCode))
            {
                string qrString = animal.Name + "&" + animal.AnimalTypeId + "&" + animal.AnimalId + "&" + animal.UserId;
                GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(qrString);
                string path = Path.Combine(_environment.WebRootPath, "GeneratedQRCode");
                
                string filePath = Path.Combine(path, qrString+".png");
                using (var stream = new FileStream(filePath, FileMode.Create,FileAccess.ReadWrite))
                {
                    barcode.Image.SaveAs(filePath);
                    //await vm.file.CopyToAsync(stream);
                    
                }
               // barcode.SaveAsPng(filePath);
                string fileName = Path.GetFileName(filePath);
                string imageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/GeneratedQRCode/" + fileName;

                findAnimal.QRCode = qrString;
                findAnimal.QRImage = imageUrl;
                _db.SaveChanges();
                return Ok(findAnimal);
            }
            else
            {
                return Ok(findAnimal);
            }
            
            
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "GenerateQR";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }

    [HttpPost("ReadQR")]
    public IActionResult ReadQR(ReadQRVM qrVM)
    {
        try
        {
            

            var findAnimal = _db.Animals.FirstOrDefault(x=>x.QRCode==qrVM.qrCode);
            findAnimal.VeterinaryId = qrVM.VeterinaryId;
            var sickness = _db.Sickess.Where(x => x.AnimalId == findAnimal.AnimalId).ToList();
            var vacciness = _db.Vaccines.Where(x => x.AnimalId == findAnimal.AnimalId).ToList();
            
            AnimalDetailVM animal = new AnimalDetailVM();
            animal.Animal = findAnimal;
            animal.Vaccines = vacciness;
            animal.Sickness = sickness;
            var vet = _db.Veterinary.FirstOrDefault(x => x.VeteniaryId == qrVM.VeterinaryId);
            
            animal.VeterinaryAccount = vet;
            var animalUser = _db.Users.FirstOrDefault(x => x.UserId == findAnimal.UserId);
            animal.UserAccount = animalUser;


            _db.SaveChanges();
            return Ok(animal);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "ReadQR";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }
}


