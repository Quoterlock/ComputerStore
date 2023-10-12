﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComputerStore.Data;
using ComputerStore.Models.Domains;
using ComputerStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ComputerStore.Models.ViewModels;

namespace ComputerStore.Controllers
{
    //[Authorize(Roles = RolesContainer.MANAGER)]
    public class CategoriesController : Controller
    {
        private IRepository<Category> _repository;

        public CategoriesController(IRepository<Category> repository)
        {
            _repository = repository;
        }

        // GET: Categories (public for other visitors)
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAll());
        }

        // GET: Categories/List
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        public async Task<IActionResult> List()
        {
            return View(await _repository.GetAll());
        }


        // GET: Categories/Details/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        public async Task<IActionResult> Details(string id)
        {
            var category = await _repository.GetById(id);
            if (category == null) return NotFound(id);
            return View(category);
        }

        // GET: Categories/Create
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryFormModel model)
        {
            if (model != null && model.Name != null && model.ThumbnailFile != null)
            {
                var category = new Category() { Name = model.Name };
                using (var stream = new MemoryStream())
                {
                    await model.ThumbnailFile.CopyToAsync(stream);
                    category.Thumbnail = new Image();
                    category.Thumbnail.Bytes = stream.ToArray();
                    category.Thumbnail.Alt = model.Name + "- Category thumbnail";
                    await _repository.Add(category);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Categories/Edit/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var category = await _repository.GetById(id);

            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Categories/Edit/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,ThumbnailImageUri")] Category category)
        {
            if (id != category.Id) return NotFound();
            try
            {
                await _repository.Update(category);
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                return View(category);
                // message
            }
        }

        // GET: Categories/Delete/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var category = await _repository.GetById(id);
            
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Categories/Delete/5
        //[Authorize(Roles = RolesContainer.MANAGER + ", " + RolesContainer.ADMINISTRATOR)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id != null)
            {
                await _repository.Delete(id);
                return RedirectToAction(nameof(List));
            }
            else
            {
                return NotFound(id);
            }
        }
    }
}
