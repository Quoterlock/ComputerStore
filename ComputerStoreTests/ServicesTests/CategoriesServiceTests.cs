using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Services;
using ComputerStore.DataAccess.Entities;
using ComputerStore.DataAccess.Interfaces;
using ComputerStoreTests.ServicesTests.FakeModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStoreTests.ServicesTests
{
    public class CategoriesServiceTests
    {
        [Fact]
        public void Get_AllCategories()
        {
            var SUT = new CategoriesService(new FakeUnitOfWork());
            var result = new List<CategoryModel> { new CategoryModel() { Id = "1", Name = "FakeCategory" } };
            var actual = SUT.GetAllAsync().Result;
            Assert.Equal(result, actual);
        }


    }
}
