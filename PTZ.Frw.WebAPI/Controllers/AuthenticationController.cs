using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PTZ.Frw.WebAPI.Library.Interfaces;
using PTZ.Frw.WebAPI.Library.Models.Authentication;
using PTZ.Frw.WebAPI.Library.Models.Users;
using PTZ.Frw.WebAPI.Library.Models.Validations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PTZ.Frw.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userManager;
        private readonly ISignInManager _signInManager;


        public AuthenticationController(IConfiguration configuration, IUserService userManager, ISignInManager signInManager)
        {
            _config = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] AuthRequest authUserRequest)
        {
            List<Validation> validations;
            UserDTO user = _signInManager.IsValidLogin(authUserRequest, out validations);
            if (user != null &&
                !validations.Any(x => x.Type == ValidationType.Error))
            {
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, authUserRequest.Username)
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

            return BadRequest("Could not verify username and password");
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            bool checkPwd = _signInManager.RegisterUser(registerRequest);
            if (checkPwd)
            {
                return Login(registerRequest as AuthRequest);
            }

            return BadRequest("Could not register user");
        }
    }
}