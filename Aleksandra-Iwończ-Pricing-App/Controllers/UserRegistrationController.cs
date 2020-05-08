using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aleksandra_Iwończ_Pricing_App.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aleksandra_Iwończ_Pricing_App.Controllers
{
    public class UserRegistrationController : Controller
    {
        private readonly ApplicationUser _controller;
        public UserRegistrationController(ApplicationUser controller)
        {
            _controller = controller;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()  // Register
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _controller.Add(user);
                _controller.SaveChanges();
                ViewBag.message = "User " + user.username + " was added!";
            }
            else
            {
                ModelState.AddModelError("", "An error occured..");
            }

            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var obj = _controller.UserReg.Where(u => u.username.Equals(user.username) && u.pass.Equals(user.pass)).FirstOrDefault();
                if (obj != null)
                {
                    return RedirectToAction("Services");
                }
                else
                {
                    ViewBag.message = "Invalid login or password, please try again.";
                }

                return View(user);
            }
            else
            {
                ModelState.AddModelError("", "An error occured.");
            }

            return View(user);
        }

        public IActionResult Services()
        {
            var multi = new MultiClass();
            if (ModelState.IsValid)
            {
                multi.projects = _controller.Project.ToList();
            }
            else
            {
                ViewBag.message = "An error ocurred";
            }
            return View(multi);
        }

      /*  [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Services(MultiClass multi)
        {
            if (ModelState.IsValid)
            {
                multi.tasks = _controller.Task.ToList();
                multi.technologies = _controller.Technology.ToList();
                multi.types = _controller.Type.ToList();

                var obj_task = _controller.Task.Where(t => t.taskName.Equals(multi.taskName)).FirstOrDefault();
                var obj_type = _controller.Type.Where(t => t.typeName.Equals(multi.typeName)).FirstOrDefault();
                var obj_tech = _controller.Technology.Where(t => t.technologyName.Equals(multi.technologyName)).FirstOrDefault();

                if (obj_task != null && obj_type != null && obj_tech != null)
                {
                    // I assume that the daily work lasts 6 hours, 2 hours for each subject
                    // Standard price is calculated based on 2 months of work, 2 months x 4 tygodnire x 5 dni x 6 godzin
                    // 240 h for a project

                    int price_daily = obj_task.pricePerHour * 2 + obj_type.pricePerHour * 2 + obj_tech.pricePerHour * 2;
                    price_daily *= 20;
                    ViewBag.message = Convert.ToString(price_daily);

                }
                else
                {
                    ViewBag.message = "Sorry, we don't have such items.";
                }
            }
            else
            {
                ViewBag.message = "An error ocurred";
            }
            return View(multi);
        }*/


        public IActionResult Estimate()  // Register
        {
            var types_db = _controller.Type.ToList();
            var techs_db = _controller.Technology.ToList();
            var tasks_db = _controller.Task.ToList();
            
            // Make Selectlist, which is IEnumerable<SelectListItem>
            var typesList = new SelectList(types_db.Select(item => new SelectListItem
            {
                Text = item.typeName,
                Value = item.typeName
            }).ToList(), "Value", "Text");

            var tasksList = new SelectList(tasks_db.Select(item => new SelectListItem
            {
                Text = item.taskName,
                Value = item.taskName
            }).ToList(), "Value", "Text");

            var techList = new SelectList(techs_db.Select(item => new SelectListItem
            {
                Text = item.technologyName,
                Value = item.technologyName
            }).ToList(), "Value", "Text");

            // Assign the Selectlist to the View Model   
            var project = new Project()
            {
                tasks = tasksList,
                technologies = techList,
                types = typesList
            };

            // return View with View Model
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Estimate(Project project)
        {
            if (ModelState.IsValid)
            {
                DateTime today = DateTime.Today;
                project.date = today.Date;
                var obj_task = _controller.Task.Where(t => t.taskName.Equals(project.taskName)).FirstOrDefault();
                var obj_type = _controller.Type.Where(t => t.typeName.Equals(project.typeName)).FirstOrDefault();
                var obj_tech = _controller.Technology.Where(t => t.technologyName.Equals(project.technologyName)).FirstOrDefault();
                int hours = project.hours;

                if (obj_task != null && obj_type != null && obj_tech != null)
                {
                    int price_all = obj_task.pricePerHour + obj_type.pricePerHour + obj_tech.pricePerHour;
                    price_all *= hours;
                    project.costs = price_all;
                    var maxId = _controller.Project.Max(table => table.projectId);
                    project.projectId = maxId + 1;
                    ViewBag.message = Convert.ToString(price_all);
                    _controller.Add(project);
                    _controller.SaveChanges();
                    ViewBag.message = "New project was added!";
                }
            }
            else
            {
                ModelState.AddModelError("", "An error occured..");
            }

            return View(project);
        }

    }
}