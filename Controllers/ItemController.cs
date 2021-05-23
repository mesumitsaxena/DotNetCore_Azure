using DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore_Azure.Controllers
{
    public class ItemController : Controller
    {
        DatabaseContext _db = new DatabaseContext();
        public IActionResult Index()
        {
            var item = _db.Items.ToList();   
            return View(item);
        }
        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                _db.Items.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            Item model = _db.Items.Find(id);

            return View("Create",model);
        }
        [HttpPost]
        public IActionResult Edit(Item model)
        {
            if (ModelState.IsValid)
            {
                _db.Items.Update(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Create", model);
        }
        public IActionResult Delete(int id)
        {
            Item model = _db.Items.Find(id);
            _db.Items.Remove(model);
            int status= _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
