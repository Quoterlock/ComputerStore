using ComputerStore.BusinessLogic.Domains;
using ComputerStore.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Interfaces
{
    public interface ICategoriesService
    {
        Task<CategoryModel> GetAsync(string id);
        Task<List<CategoryModel>> GetAllAsync();
        Task AddAsync(CategoryModel category);
        Task UpdateAsync(CategoryModel category);
        Task RemoveAsync(string id);
        Task<bool> IsExistsAsync(string id);
        Task<List<CategoryModel>> SearchAsync(string value);
    }
}
