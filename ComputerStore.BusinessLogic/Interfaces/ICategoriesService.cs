using ComputerStore.BusinessLogic.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Interfaces
{
    public interface ICategoriesService
    {
        Task<CategoryModel> Get(string id);
        Task<List<CategoryModel>> GetAll();
        Task Add(CategoryModel category);
        Task Update(CategoryModel category);
    }
}
