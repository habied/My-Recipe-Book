using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RecipeBook.Core.Entities;
using RecipeBook.Core.Interfaces;
using RecipeBookApp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace RecipeBook.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IUserUOW _UnitOfWork;

        public AuthController(IOptions<AppSettings> appSettings, IUserUOW unitOfWork)
        {
            _appSettings = appSettings.Value;
            _UnitOfWork = unitOfWork;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] string credential, CancellationToken cancellationtoken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(credential);

                var user = await _UnitOfWork.Users.GetByIdWithIncludesAsync(x => x.UserName == payload.Name, cancellationtoken);
                if (user is null)
                {
                    var userId = await _UnitOfWork.Users.AddAsync(new Core.Entities.User
                    {
                        Id = ShortGUID(),
                        UserName = payload.Name,
                    });
                    
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this._appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new[] { new Claim("id", user.UserName) }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)

                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var encrypterToken = tokenHandler.WriteToken(token);
                return Ok(new { token = encrypterToken, username = user.UserName });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        public static string ShortGUID()
        {
            return Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
        }
    }
}
