using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.Identity.API.Resources;
using NETCore.Identity.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        public AuthController(
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(UserSignUpResource userSignUpResource)
        {

            //Map the Email as an UserName
            var user = _mapper.Map<UserSignUpResource, User>(userSignUpResource);

            //Create the user with the specified parameters
            var userCreateResult = await _userManager.CreateAsync(user, userSignUpResource.Password);

            //Verify if the creation of the user was succesful
            if (userCreateResult.Succeeded)
            {
                return Created(string.Empty, string.Empty);
            }

            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInResource signInResource)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == signInResource.Email);

            if (user is null)
            {
                return NotFound("User not found");
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, signInResource.Password);

            if (userSigninResult)
            {
                return Ok();
            }

            return BadRequest("Email or password incorrect.");
        }

        #region Roles

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Insert a valid role name.");
            }

            var newRole = new Role
            {               
                Name = roleName
            };       
                       
            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return CreatedAtAction(nameof(CreateRole), new { id = newRole.Id }, newRole);
            }

            return Problem(roleResult.Errors.First().Description, null, 500);
        }

        [HttpPost("AddUserToRole/{userEmail}")]
        public async Task<IActionResult> AddUserToRole(string userEmail, [FromBody] string roleName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userEmail);

            if (user is null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(AddUserToRole), result);
            }

            return Problem(result.Errors.First().Description, null, 500);
        }

        #endregion
    }
}
