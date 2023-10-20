using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using ComputerStore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerStore.BusinessLogic.Services
{
    public class CategoriesService : ICategoriesService
    {
        private IUnitOfWork _unitOfWork;
        public CategoriesService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CategoryModel> Get(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                var category = await _unitOfWork.Categories.GetById(id);
                if (category != null)
                    return category;
                else throw new Exception("Category not found by id: " + id);
            }
            else throw new Exception("Category id is null or empty");
        }
    }
}
