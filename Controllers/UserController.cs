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
        try
        {
            var userNew = _db.Users.FirstOrDefault(x => x.Username == user.Username || x.Email == user.Email);
            if (!String.IsNullOrEmpty(user.Username))
            {
                if (userNew == null)
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    if (user.UserTypeId == 2)
                    {
                        Veterinary veterinary = new Veterinary();
                        veterinary.CityId = user.CityId;
                        veterinary.DistrictId = user.DistrictId;
                        veterinary.userId = user.UserId;
                        veterinary.Email = user.Email;
                        _db.Veterinary.Add(veterinary);
                        _db.SaveChanges();
                    }

                    if (user.UserTypeId == 4)
                    {
                        PetHotel petHotel = new PetHotel();
                        petHotel.Name = user.Name;
                        petHotel.CityId = user.CityId;
                        petHotel.DistrictId = user.DistrictId;
                        petHotel.UserId = user.UserId;
                        petHotel.Address = user.Address;
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
                        animalWalker.Address = user.Address;
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
            else
            {
                return BadRequest("Kullanıcı Adı Boş");
            }
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            
            log.LogType = "Register";
            log.UserId = user.UserId;
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
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
    [HttpGet("UpdateToken")]
    public IActionResult UpdateToken(int userId,string token)
    {
        try
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == userId).Token = token;
            _db.SaveChanges();
            return Ok(user);
        }
        catch (Exception ex)
        {
            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message+"StackTrace :"+ex.StackTrace;
            log.LogType = "UpdateToken";
            log.UserId = userId;
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
            if (user.Username != oldUser.Username && user.Email != oldUser.Email)
            {
                var controlUser = _db.Users.FirstOrDefault(x => (x.Username == user.Username || x.Email == user.Email)&&(x.Username!=oldUser.Username||x.Email!=oldUser.Email));
                if (controlUser.Username != null || controlUser.Username != "")
                {
                    return BadRequest("Bu Kullanıcı bilgilerine sahip bir kullanıcı mevcut bunu gerçekleştiremezsiniz.");
                }
                else
                {
                    oldUser.Username = user.Username;
                    oldUser.Address = user.Address;
                    oldUser.CityId = user.CityId;
                    oldUser.DistrictId = user.DistrictId;
                    oldUser.Desc = user.Desc;
                    oldUser.Email = user.Email;
                    oldUser.Name = user.Name;
                    oldUser.Password = user.Password;
                    oldUser.Surname = user.Surname;
                    _db.SaveChanges();
                    return Ok(oldUser);
                }
            }
            else if (user.Username != oldUser.Username)
            {
                oldUser.Username = user.Username;
                oldUser.Address = user.Address;
                oldUser.CityId = user.CityId;
                oldUser.DistrictId = user.DistrictId;
                oldUser.Desc = user.Desc;
                oldUser.Name = user.Name;
                oldUser.Password = user.Password;
                oldUser.Surname = user.Surname;
                _db.SaveChanges();
                return Ok(oldUser);
            }
            else if (user.Email != oldUser.Email)
            {
                oldUser.Address = user.Address;
                oldUser.CityId = user.CityId;
                oldUser.DistrictId = user.DistrictId;
                oldUser.Desc = user.Desc;
                oldUser.Email = user.Email;
                oldUser.Name = user.Name;
                oldUser.Password = user.Password;
                oldUser.Surname = user.Surname;
                _db.SaveChanges();
                return Ok(oldUser);
            }
                else
                {
                    oldUser.Username = user.Username;
                    oldUser.Address = user.Address;
                    oldUser.CityId = user.CityId;
                    oldUser.DistrictId = user.DistrictId;
                    oldUser.Desc = user.Desc;
                    oldUser.Email = user.Email;
                    oldUser.Name = user.Name;
                    oldUser.Password = user.Password;
                    oldUser.Surname = user.Surname;
                    _db.SaveChanges();
                    return Ok(oldUser);
                }
            
            
            
            
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
    [HttpGet("GetUserDetail")]
    public IActionResult GetUserDetail(int userId)
    {
        try
        {
            var user = _db.Users.FirstOrDefault(x => x.UserId == userId);
            return Ok(user);
        }
        catch (Exception ex)
        {

            Log log = new Log();
            log.DateTime = DateTime.Now;
            log.LogDescription = ex.Message;
            log.LogType = "GetUserDetail";
            log.UserId = userId;
            _db.Logs.Add(log);
            _db.SaveChanges();
            return BadRequest(ex);
        }
        
    }
    [HttpGet("GetUserDetailFromUsername")]
    public IActionResult GetUserDetailFromUsername(string username)
    {
        try
        {
            var user = _db.Users.FirstOrDefault(x => x.Username == username);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}