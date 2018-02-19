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
            var controller = new ValuesController();

            Assert.IsTrue(controller.Get(1) == "value");
        }
    }
}
