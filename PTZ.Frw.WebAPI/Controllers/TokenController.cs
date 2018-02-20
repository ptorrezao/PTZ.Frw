using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PTZ.Frw.DataAccess.Interfaces;
using PTZ.Frw.DataAccess.Models;
using PTZ.Frw.WebAPI.Interfaces;
using PTZ.Frw.WebAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace PTZ.Frw.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserManager _userManager;
        private readonly ISignInManager _signInManager;

        public TokenController(IConfiguration configuration, IUserManager userManager, ISignInManager signInManager)
        {
            _config = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] AuthRequest authUserRequest)
        {
            User user = _userManager.FindByEmail(authUserRequest.UserName);

            if (user != null)
            {
                bool checkPwd = _signInManager.CheckPasswordSignIn(user, authUserRequest);
                if (checkPwd)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, authUserRequest.UserName)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _config["Tokens:Issuer"],
                        audience: _config["Tokens:Issuer"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
                }
            }

            return BadRequest("Could not verify username and password");
        }
    }
}