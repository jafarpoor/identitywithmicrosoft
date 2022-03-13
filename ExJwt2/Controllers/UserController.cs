using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjctModel.Entites;
using ProjctModel.Services;
using ProjctModel.VeiwModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExJwt2.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager ,
                              RoleManager<Role> roleManager ,
                              SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;


        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post(UserDto userDto)
        {
            var user = new User
            {
                Email = userDto.Email,
                FullName = userDto.FullName,
                UId = Guid.NewGuid(),
                UserName = userDto.UserName ,
                IsActive = true
            };

            var UserResult = await _userManager.CreateAsync(user, userDto.Password);
            if (!UserResult.Succeeded)
                return Conflict(UserResult.Errors.Select(x => x.Description).ToList());

            var role = await _roleManager.FindByNameAsync(userDto.Role);
            if (role == null)
               await _roleManager.CreateAsync(new Role { Name = userDto.Role });

            var newUser = await _userManager.FindByNameAsync(userDto.UserName);
            var newRole = await _roleManager.FindByNameAsync(userDto.Role);

            return Ok(UserResult.Succeeded);
        }

        [HttpGet]
        [Route("Login")]
        public async  Task<IActionResult> Login(string UserName , string Password)
        {
            var resulte = await _userManager.FindByNameAsync(UserName);
            if (resulte == null)
                return NotFound();

            var LoginResult = await _signInManager.CheckPasswordSignInAsync(resulte, Password ,false);
            if (!LoginResult.Succeeded)
                return Conflict("invalid password");

            var calims = await _signInManager.ClaimsFactory.CreateAsync(resulte);

            return Ok(SampleJwt.GetToke(resulte , calims.Claims.ToList()));
        }
    }
}
