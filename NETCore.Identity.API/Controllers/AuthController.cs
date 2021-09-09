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
        public AuthController(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserSignUpResource userSignUpResource)
        {        
            
          var user = _mapper.Map<UserSignUpResource, User>(userSignUpResource);

            var userCreateResult = await _userManager.CreateAsync(user, userSignUpResource.Password);

            if (userCreateResult.Succeeded)
            {
                return Created(string.Empty, string.Empty);
            }

            return Problem(userCreateResult.Errors.First().Description, null, 500);
        }
    }
}
