using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChoETL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OLXFakedBackend.configuration;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Api;
using OLXFakedBackend.Models.Api.Authentication.Requests;
using OLXFakedBackend.Models.Db;
using OLXFakedBackend.Utils;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OLXFakedBackend.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerWithJwt
    {

        public UsersController(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            IRepositoryWrapper repositoryWrapper,
            TokenValidationParameters tokenValidationParameters
            ) : base(
                userManager: userManager,
                optionsMonitor: optionsMonitor,
                repositoryWrapper: repositoryWrapper,
                tokenValidationParameters: tokenValidationParameters
                ) {}

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> userRegister([FromBody] UserRegistrationRequest _user)
        {
                // check i the user with the same email exist
                var existingUser = await _userManager.FindByEmailAsync(_user.userId);

                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>(){"Email already exist"}
                    });
                }

                var newUser = new IdentityUser() { Email = _user.email, UserName = _user.userId, PhoneNumber= _user.phoneNumber };
                var isCreated = await _userManager.CreateAsync(newUser, _user.password);
                if (isCreated.Succeeded) {
                
                    var jwtToken = (await GenerateJwtToken(newUser)).Token;
                    var contactPersonCity = (await _repositoryWrapper.CityRepository.FindByConditions(new List<System.Linq.Expressions.Expression<Func<Models.City, bool>>>() { c => c.Name == _user.contactCity })).FirstOrDefault();

                    var contactPerson = new Models.ContactPerson { Name = _user.contactPersonName, CityId= contactPersonCity.CityId };

                    var unitedUser = new UserUnited { UserId = newUser.Id, ContactPerson = contactPerson };

                    if(_user.image != null)
                    {
                        string serverPath = ImageUtil.GetUserPicPath(newUser.Id);
                        ImageUtil.PlaceImage(_user.image.base64Content, serverPath, _user.image.fileName);


                        unitedUser.Image = new Models.Image { Path = ImageUtil.GetUserImageApiPath(newUser.Id, _user.image.fileName) };
                    }

                    if(_user.lowName != null)
                    {
                        var userRequisites = new Requisites {
                            LowName = _user.lowName,
                            LowAddress = _user.lowAddress,
                            ZipCode = (int)_user.zipCode,
                            CityId = (await _repositoryWrapper.CityRepository.FindByConditions(new List<System.Linq.Expressions.Expression<Func<Models.City, bool>>>() { c => c.Name == _user.requisitesCity })).FirstOrDefault().CityId,
                            SingleRegId = _user.singleRegId,
                            IsTaxesPayer = (bool)_user.isTaxesPayer,
                            TaxationId = (int)_user.taxationId,
                            ContactPersonName = _user.requisitesContactPersonName
                        };

                    unitedUser.Requisites = userRequisites;
                    }

                    await _repositoryWrapper.UserUnitedRepository.Create(unitedUser);
                    await _repositoryWrapper.SaveAsync();

                    return Ok(new RegistrationResponse()
                    {
                        Result = true,
                        Token = jwtToken
                    });
                }

                return new JsonResult(new RegistrationResponse()
                {
                    Result = false,
                    Errors = isCreated.Errors.Select(x => x.Description).ToList()
                }
                        )
                { StatusCode = 500 };
        }


        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> userLogin([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                // check if the user with the same email exist
                var existingUser = await _userManager.FindByEmailAsync(user.UserId);

                if (existingUser == null)
                {
                    // We dont want to give to much information on why the request has failed for security reasons
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>(){
                                        "Invalid authentication request"
                                    }
                    });
                }

                // Now we need to check if the user has inputed the right password
                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (isCorrect)
                {
                    var jwtToken = await GenerateJwtToken(existingUser);

                    return Ok(new RegistrationResponse()
                    {
                        Result = true,
                        Token = jwtToken.Token,
                        RefreshToken = jwtToken.RefreshToken
                    });
                }
                else
                {
                    // We dont want to give to much information on why the request has failed for security reasons
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>(){ "Invalid authentication request" }
                    });
                }
            }

            return BadRequest(new RegistrationResponse()
            {
                Result = false,
                Errors = new List<string>(){ "Invalid payload" }
            });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("current_user")]
        [HttpPost]
        public async Task<IActionResult> GetCurrentUser() {
            var userid = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            var _curUser = (await _repositoryWrapper.UserPreferencesRpository.FindByConditions(new List<System.Linq.Expressions.Expression<Func<Models.Api.Product.Responses.UserPreferences, bool>>>() { up => up.UserId == userid })).FirstOrDefault();
            return Ok(_curUser);
        }

        [Produces(MediaTypeNames.Application.Json)]
        [Route("{id}")]
        [HttpGet]
        public string GetUserById(int id)
        {
            return $"this user with id# {id}";
        }


    }
}

