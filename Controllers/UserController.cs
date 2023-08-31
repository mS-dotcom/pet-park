using Microsoft.AspNetCore.Mvc;
using perpark_api.DevelopmentClasses;
using perpark_api.Models;
using perpark_api.Models.Entities;

namespace perpark_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    
    private readonly ILogger<UserController> _logger;
    Models.Context _db = new Models.Context();

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpPost("Register")]
    public IActionResult Register(Models.User user)
    {
        var userNew = _db.Users.FirstOrDefault(x => x.Username == user.Username & x.Email == user.Email);
        if (!String.IsNullOrEmpty(user.Username))
        {
            _db.Users.Add(user);
            if (user.UserTypeId == 2)
            {
                Veterinary veterinary = new Veterinary();
                veterinary.CityId = user.CityId;
                veterinary.DistrictId = user.DistrictId;
                veterinary.userId = user.UserId;
                _db.Veterinary.Add(veterinary);
            }

            if (user.UserTypeId == 4)
            {
                PetHotel petHotel = new PetHotel();
                petHotel.CityId = user.CityId;
                petHotel.DistrictId = user.DistrictId;
                petHotel.UserId = user.UserId;
                _db.PetHotels.Add(petHotel);
            }

            if (user.UserTypeId == 3)
            {
                AnimalWalker animalWalker = new AnimalWalker();
                animalWalker.CityId = user.CityId;
                animalWalker.DistrictId = user.DistrictId;
                animalWalker.UserId = user.UserId;
                animalWalker.Lat = user.Lat;
                animalWalker.Lng = user.Lng;
                _db.AnimalWalkers.Add(animalWalker);
            }

            _db.SaveChanges();
            return Ok(user);
        }
        else
        {
            return BadRequest("Bu Kullanıcı Zaten Mevcut");
            
        }
        
    }
    [HttpPost("AddUserType")]
    public IActionResult AddUserType(UserType userType)
    {
        _db.UserTypes.Add(userType);
        _db.SaveChanges();
        return Ok(userType);
    }
    [HttpPost("Login")]
    public IActionResult Login(UserLoginVM user)
    {
        try
        {
            var userexample = _db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if (!String.IsNullOrEmpty(userexample.Name))
            {
                return Ok(userexample);
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
            log.LogType = "Login";
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }

    [HttpPost("UpdateProfile")]
    public IActionResult UpdateProfile(User user)
    {
        try
        {
            var oldUser = _db.Users.FirstOrDefault(x => x.UserId == user.UserId);
            oldUser.Address = user.Address;
            oldUser.CityId = user.CityId;
            oldUser.DistrictId = user.DistrictId;
            oldUser.Desc = user.Desc;
            oldUser.Email = user.Email;
            oldUser.Name = user.Name;
            oldUser.Password = user.Password;
            oldUser.Surname = user.Surname;
            oldUser.Username = user.Username;
            _db.SaveChanges();
            return Ok(oldUser);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "UpdateProfile";
            log.UserId = user.UserId;
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }
}

