using ComputerStore.Areas.Customer.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStoreTests.ControllersTests.Customer
{
    public class UnitTest1
    {
        [Fact]
        public void Test_Index_ReturnsViewName()
        {
            var controller = new HomeController(null);
            var result = controller.Index() as ViewResult;
            Assert.Equal("Index", result?.ViewName);
        }

        [Fact]
        public void ActionTest2() { }
    }
}