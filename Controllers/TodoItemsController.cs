using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TodoItemsController : Controller
    {
        private ApplicationDBContext _dbContext = new ApplicationDBContext();
        public IActionResult Items(string name)
        {
            var items = _dbContext.Items.ToList();
            
            ViewBag.name=name;
            return View(items);
        }

        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(string title, string description, DateTime deadline)
        {
            _dbContext.Items.Add(new()
            {
                Title = title,
                Description = description,
                DeadLine = deadline
                
            });

            _dbContext.SaveChanges();

            TempData["success"] = "Add New Items successfuly";

            return RedirectToAction(nameof(Items));
        }




        public IActionResult Edit(int personId)
        {
            var person = _dbContext.Items.Find(personId);
            if (person != null) return View(person);
            return RedirectToAction("Items");
        }

        [HttpPost]
        public IActionResult Edit( int personId ,string title, string description, DateTime deadline)
        {
            _dbContext.Items.Update(new(){
                Id = personId,
                Title=title,
                Description=description,
                DeadLine=deadline
            });
            _dbContext.SaveChanges();
            TempData["success"] = "Edit Items successfuly";
            return RedirectToAction("Items");
        }

        public IActionResult Delete(int personId)
        {
            var person = _dbContext.Items.Find(personId);
            if (person != null)
            {
                _dbContext.Items.Remove(person);
                _dbContext.SaveChanges();
                TempData["success"] = "Delete Item successfuly";
                return RedirectToAction("Items");
            }
            return RedirectToAction("NotFoundPage");
        }



        public IActionResult NotFoundPage()
        {
            return View();
        }



    }
}
