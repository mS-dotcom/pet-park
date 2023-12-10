using Microsoft.AspNetCore.Mvc;
using perpark_api.DevelopmentClasses;
using perpark_api.Models;
using perpark_api.Models.Entities;

namespace perpark_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalController : ControllerBase
{
    
    private readonly ILogger<AnimalController> _logger;
    Models.Context _db = new Models.Context();

    public AnimalController(ILogger<AnimalController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetAllAnimalTypes")]
    public IActionResult GetAllAnimalTypes()
    {
        try
        {
            var model = _db.AnimalType.ToList();
            return Ok(model);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "GetAllAnimalTypes";
            
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }
    [HttpPost("AddAnimal")]
    public IActionResult AddAnimal(Animal Animal)
    {
        try
        {
            var animalTypeText = _db.AnimalType.FirstOrDefault(x => x.AnimalTypeId == Animal.AnimalTypeId).TypeName;
            Animal.AnimalTypeText = animalTypeText;
            _db.Animals.Add(Animal);
            _db.SaveChanges();
            return Ok(Animal);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "AddAnimal";
            
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }   
    }

    [HttpPost("EditAnimal")]
    public IActionResult EditAnimal(Animal animal)
    {
        try
        {
            var oldAnimal = _db.Animals.FirstOrDefault(x => x.AnimalId == animal.AnimalId);
            if (animal.AnimalAge != 0 && animal.AnimalAge != null)
            {
                oldAnimal.AnimalAge = animal.AnimalAge;
            }
            if (!String.IsNullOrEmpty(oldAnimal.Name))
            {
                oldAnimal.Name = animal.Name;
            }
            
            oldAnimal.Gender = animal.Gender;
            
            _db.SaveChanges();
            return Ok(oldAnimal);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "EditAnimal";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }

    [HttpGet("DeleteAnimal")]
    public IActionResult DeleteAnimal(int AnimalId)
    {
        try
        {
            var sicks = _db.Sickess.Where(x => x.AnimalId == AnimalId).ToList();
            var vac = _db.Vaccines.Where(x => x.AnimalId == AnimalId).ToList();
            if (sicks.Count > 0)
            {
                foreach (var item in sicks)
                {
                    _db.Sickess.Remove(item);
                }
            }
            if (vac.Count > 0)
            {
                foreach (var item in vac)
                {
                    _db.Vaccines.Remove(item);
                }
            }
            
            var animal = _db.Animals.FirstOrDefault(x => x.AnimalId == AnimalId);
            _db.Animals.Remove(animal);
            _db.SaveChanges();
            return Ok(animal);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "DeleteAnimal";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }
    [HttpPost("AddAnimalType")]
    public IActionResult AddAnimalType(AnimalType animalType)
    {
        try
        {
            _db.AnimalType.Add(animalType);
            _db.SaveChanges();
            return Ok(animalType);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "AddAnimalType";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
    }
    [HttpGet("MyPets")]
    public IActionResult MyPets(int userId,int userType)
    {
        try
        {
            if (userType == 1)
            {
                var mypets = _db.Animals.Where(x => x.UserId == userId).ToList();
                return Ok(mypets);
            }
            else if (userType == 2)
            {
                var vet = _db.Veterinary.FirstOrDefault(x => x.userId == userId);
                var mypets = _db.Animals.Where(x => x.VeterinaryId == vet.VeteniaryId).ToList();
                return Ok(mypets);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "MyPets";
            log.UserId = userId;
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }
    [HttpGet("GetAnimalDetail")]
    public IActionResult GetAnimalDetail(int AnimalId)
    {
        try
        {
            var animal = _db.Animals.FirstOrDefault(x => x.AnimalId == AnimalId);
            var animalSick = _db.Sickess.Where(x => x.AnimalId == AnimalId).ToList();
            var animalVaccines = _db.Vaccines.Where(x => x.AnimalId == AnimalId).ToList();
            var user = _db.Users.FirstOrDefault(x => x.UserId == animal.UserId);
            var veterinary = _db.Veterinary.FirstOrDefault(x => x.VeteniaryId == animal.VeterinaryId);
            AnimalDetailVM vm = new AnimalDetailVM();
            vm.Animal = animal;
            vm.Sickness = animalSick;
            vm.Vaccines = animalVaccines;
            vm.UserAccount = user;
            vm.VeterinaryAccount = veterinary;
            return Ok(vm);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "GetAnimalDetail";
            
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        

    }
    [HttpGet("GetAnimalDetailFromQR")]
    public IActionResult GetAnimalDetailFromQR(string QRCode)
    {
        try
        {
            
            

            var findAnimal = _db.Animals.FirstOrDefault(x=>x.QRCode==QRCode);



            
            var animalSick = _db.Sickess.Where(x => x.AnimalId == findAnimal.AnimalId).ToList();
            var animalVaccines = _db.Vaccines.Where(x => x.AnimalId == findAnimal.AnimalId).ToList();
            var user = _db.Users.FirstOrDefault(x => x.UserId == findAnimal.UserId);
            var veterinary = _db.Veterinary.FirstOrDefault(x => x.VeteniaryId == findAnimal.VeterinaryId);
            AnimalDetailVM vm = new AnimalDetailVM();
            vm.Animal = findAnimal;
            vm.Sickness = animalSick;
            vm.Vaccines = animalVaccines;
            vm.UserAccount = user;
            vm.VeterinaryAccount = veterinary;
            return Ok(vm);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "GetAnimalDetailFromQR";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }


    }

}

