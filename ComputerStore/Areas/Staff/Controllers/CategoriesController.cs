using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ComputerStore.Areas.Staff.ViewModels;
using ComputerStore.BusinessLogic.Domains;
using ComputerStore.BusinessLogic.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Drawing;
using System.Collections;
using ComputerStore.DataAccess.Entities;
using ComputerStore.Utilities;

namespace ComputerStore.Areas.Staff.Controllers
{
    [Area("Staff")]
    [Authorize(Roles = RolesContainer.MANAGER + "," + RolesContainer.ADMINISTRATOR)]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoriesService.GetAllAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CategoryFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryFormModel model)
        {
            if (ModelIsValid(model))
            {
                var category = model.Category;
                if (category != null && model.ThumbnailFile != null)
                {
                    category.Thumbnail = new ImageModel
                    {
                        Bytes = ConvertFileToBytes(model.ThumbnailFile),
                        Alt = category.Name + "-category-icon"
                    };
                    await _categoriesService.AddAsync(category);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try 
            {
                var category = await _categoriesService.GetAsync(id);
                var model = new CategoryFormModel(){
                    Category = category
                };
                return View(model);

            } catch (Exception ex) 
            { 
                return ErrorMessage(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryFormModel model)
        {
            if(ModelIsValid(model))
            {
                if (model.Category != null && await _categoriesService.IsExistsAsync(model.Category.Id??string.Empty))
                {
                    try
                    {
                        var category = model.Category;
                        if (model.ThumbnailFile != null)
                        {
                            category.Thumbnail = new ImageModel()
                            {
                                Bytes = ConvertFileToBytes(model.ThumbnailFile),
                                Alt = category.Name + "-category-icon"
                            };
                            await _categoriesService.UpdateAsync(category);
                            return RedirectToAction(nameof(Index));
                        }
                        return View(nameof(Edit), model);
                    }
                    catch (Exception ex)
                    {
                        return ErrorMessage(ex);
                    }
                }
            } 
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return View(await _categoriesService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(CategoryModel model)
        {
            if(!string.IsNullOrEmpty(model.Id))
            {
                var category = await _categoriesService.GetAsync(model.Id);
                if (category != null && !string.IsNullOrEmpty(category.Id))
                    await _categoriesService.RemoveAsync(category.Id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string value)
        {
            if(!string.IsNullOrEmpty(value))
            {
                ViewData["SearchValue"] = value;
                return View(nameof(Index), await _categoriesService.Search(value));
            }
            return RedirectToAction();
        }

        private static byte[] ConvertFileToBytes(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private static bool ModelIsValid(CategoryFormModel model)
        {
            return model != null && model.Category != null && !string.IsNullOrEmpty(model.Category.Name);
        }

        private IActionResult ErrorMessage(Exception ex)
        {
            return View("Error", ex.Message);
        }
    }
}
