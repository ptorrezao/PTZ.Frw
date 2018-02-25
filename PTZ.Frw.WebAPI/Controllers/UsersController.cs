using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTZ.Frw.WebApi.Services.UserManager;
using PTZ.Frw.WebAPI.Interfaces;
using PTZ.Frw.WebAPI.Models.Users;
using PTZ.Frw.WebAPI.Utils;

namespace PTZ.Frw.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userManager;

        public UsersController(
            IHttpContextAccessor httpContextAccessor,
            IUserService userManager) :
            base(httpContextAccessor)
        {
            _userManager = userManager;
        }

        // GET api/values
        [HttpGet]
        [Authorize]
        public IEnumerable<UserDTO> Get()
        {
            return _userManager.GetUsers();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public UserDTO Get(int id)
        {
            return _userManager.GetUser(id);
        }

        // POST api/values
        [HttpPost]
        [Obsolete]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
