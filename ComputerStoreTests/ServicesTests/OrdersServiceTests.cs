using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Services;
using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using ComputerStoreTests.ServicesTests.FakeModules;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreTests.ServicesTests
{
    public class OrdersServiceTests
    {
        Mock<IUnitOfWork> mockUow;
        Mock<IOrdersRepository> mockRepository;
        public OrdersServiceTests() {
            mockUow = new Mock<IUnitOfWork>();
            mockRepository = new Mock<IOrdersRepository>();
            mockUow.Setup(u=>u.Orders).Returns(mockRepository.Object);
        }

        [Fact]
        public void SearchOrders_By_Status()
        {
            
            var orders = new List<Order>() { new Order { Id = new Guid().ToString(), Status = "Pending" } };
            var model = new OrderModel { Id = "1", Status = ComputerStore.Utilities.OrderStatus.Pending };
            mockRepository.Setup(e => e.GetAsync()).ReturnsAsync(orders);
            var SUT = new OrdersService(mockUow.Object);
            var result = SUT.Search("1").Result;

            Assert.Equal(model, result.First());
        }
    }
}
