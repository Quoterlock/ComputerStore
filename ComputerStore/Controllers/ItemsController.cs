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
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputerStore.Controllers
{
    //[Authorize(Roles = RolesContainer.MANAGER)]
    public class ItemsController : Controller
    {
        private readonly IRepository<Item> _repository;

        public ItemsController(IRepository<Item> repository)
        {
            _repository = repository;
        }

        // GET: Items/?categoryId
        public async Task<IActionResult> Index(string categoryId)
        {
            List<Item> items;
            if (categoryId != null)
            {
                items = await _repository.Get(item => item.Category.Id == categoryId);
                if (items != null && items.Count > 0) 
                    ViewData["CategoryName"] = items[0].Category.Name;
            }
            else
            {
                items = await _repository.GetAll();
                ViewData["CategoryName"] = "All";
            }

            if(items == null)
                items = new List<Item>();

            return View(items);

        }

        public async Task<IActionResult> Search(string value)
        {
            var items = await _repository.FindAll(value);
            ViewData["SearchValue"] = value;
            return View(items);
        }

        // Get: Items/List
        public async Task<IActionResult> List()
        {
            return View(await _repository.GetAll());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var item = await _repository.GetById(id);
            if(item == null) 
                return NotFound();
            else 
                return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImageUri,Price")] Item item)
        {
            if (item.Name != null)
            {
                await _repository.Add(item);
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id != null)
            {
                var item = await _repository.GetById(id);
                if (item != null)
                    return View(item);
            }
            return NotFound();
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Description,ImageUri,Price")] Item item)
        {
            if(id != null && id != item.Id && item.Name != null)
            {
                await _repository.Update(item);
                return RedirectToAction(nameof(List));
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if(id != null && id != string.Empty)
            {
                var item = await _repository.GetById(id);
                if (item != null) return View(item);
            }
            return NotFound();
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id != null && id != string.Empty)
            {
                await _repository.Delete(id);
                return RedirectToAction(nameof(List));
            }
            return NotFound();
        }
    }
}