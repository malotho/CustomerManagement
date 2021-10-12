using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using LogicLayer.Models;
using LogicLayer;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Microsoft.Extensions.Options;

namespace CustomerManagementPortal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IUserService _userService;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IOptions<AppSettings> appSettings
            )
        {
            _appSettings = appSettings.Value;
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = _userService.GetAll().ToList();
            return users;
        }

        [AllowAnonymous]
        // GET: api/Users
        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<User>>> GetPagedUsers(int pageNo, int pageSize)
        {
            var x = pageNo - 1;
            var users = _userService.GetAll()
                .Skip(x * pageSize)
                .Take(pageSize)
                .ToList();
            return users;
        }

        [HttpGet("UserCount")]
        public int UsersCount()
        {
            return _userService.UsersCount();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            try
            {
                _userService.Update(user);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> register(User user)
        {
            _userService.Add(user);
            return CreatedAtAction("GetUser", new { id = user.id }, user);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]

        public async Task<ActionResult<User>> authenticate(User u)
        {
            var user = _userService.Authenticate(u.username, u.password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.id,
                Username = user.username,
                FirstName = user.firstName,
                LastName = user.lastName,
                Token = tokenString
            });
        }
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _userService.Delete(id);
            return Accepted();
        }
    }
}
