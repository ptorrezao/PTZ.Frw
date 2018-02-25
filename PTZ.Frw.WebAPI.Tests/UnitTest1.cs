using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTZ.Frw.DataAccess;
using PTZ.Frw.WebApi.Services.UserManager;
using PTZ.Frw.WebAPI.Controllers;
using PTZ.Frw.WebAPI.Models.Users;
using System.Collections.Generic;

namespace PTZ.Frw.WebAPI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private UserRepository userRepo;
        private UserService userManager;

        [ClassInitialize]
        public void ClassInitializer()
        {
            //userRepo = new UserRepository();
            //userManager = new UserService(userRepo);
        }

        [TestMethod]
        public void ListUsers()
        {
            //UsersController controller = new UsersController(null, userManager);
            //IEnumerable<UserDTO> users = controller.Get();
        }
    }
}
