using BrewCoffee.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BrewCoffee.Tests
{
    [TestClass]
    public sealed class BrewCoffeeTests
    {
        [TestMethod]
        public void BrewCoffee_AprilFirst_Returns418()
        {
            TestDateTimeProvider testDate = new TestDateTimeProvider();
            testDate.TestDate = new DateTime(2026, 4, 1);

            BrewCoffeeService service = new BrewCoffeeService(testDate);
            BrewCoffeeController controller = new BrewCoffeeController(service, testDate);

            StatusCodeResult? res = controller.Brew() as StatusCodeResult;

            Assert.IsNotNull(res);
            Assert.AreEqual(418, res.StatusCode);
        }

        [TestMethod]
        public void BrewCoffee_EveryFifthCall_Returns503()
        {
            TestDateTimeProvider testDate = new TestDateTimeProvider();
            testDate.TestDate = new DateTime(2026, 4, 7);

            BrewCoffeeService service = new BrewCoffeeService(testDate);
            BrewCoffeeController controller = new BrewCoffeeController(service, testDate);

            for(int i = 0; i < 4; i++) 
                controller.Brew();

            StatusCodeResult? res = controller.Brew() as StatusCodeResult; 

            Assert.IsNotNull(res);
            Assert.AreEqual(503, res.StatusCode);
        }

        [TestMethod]
        public void BrewCoffee_RegularDay_Returns200()
        {
            TestDateTimeProvider testDate = new TestDateTimeProvider();
            testDate.TestDate = new DateTime(2026, 4, 7);

            BrewCoffeeService service = new BrewCoffeeService(testDate);
            BrewCoffeeController controller = new BrewCoffeeController(service, testDate);

            OkObjectResult? res = controller.Brew() as OkObjectResult;
            string? msg = res?.Value.GetType().GetProperty("message").GetValue(res?.Value).ToString();

            Assert.IsNotNull(res);
            Assert.AreEqual(200, res.StatusCode);
            Assert.AreEqual("Your piping hot coffee is ready", msg);
        }
    }
}
