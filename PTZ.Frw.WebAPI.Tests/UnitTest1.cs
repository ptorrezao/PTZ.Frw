using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTZ.Frw.WebAPI.Controllers;

namespace PTZ.Frw.WebAPI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetById()
        {
            var controller = new UsersController(null, null);

            Assert.IsTrue(controller.Get(1) == null);
        }
    }
}
