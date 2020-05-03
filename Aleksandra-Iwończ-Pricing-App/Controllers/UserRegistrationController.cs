using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aleksandra_Iwończ_Pricing_App.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Session;

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
                multi.tasks = _controller.Task.ToList();
                multi.technologies = _controller.Technology.ToList();
                multi.types = _controller.Type.ToList();
            }
            else
            {
                ViewBag.message = "An error ocurred";
            }
            return View(multi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Services(MultiClass multi)
        {
            if (ModelState.IsValid)
            {
                multi.tasks = _controller.Task.ToList();
                multi.technologies = _controller.Technology.ToList();
                multi.types = _controller.Type.ToList();

                var obj_task = _controller.Task.Where(t => t.taskId.Equals(multi.taskId)).FirstOrDefault();
                var obj_type = _controller.Type.Where(t => t.typeId.Equals(multi.typeId)).FirstOrDefault();
                var obj_tech = _controller.Technology.Where(t => t.technologyId.Equals(multi.technologyId)).FirstOrDefault();

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
        }

      /*  public IActionResult EstimateCost(MultiClass multi)
        {
            var obj_task = _controller.Task.Where(t => t.taskId.Equals(multi.taskId)).FirstOrDefault();
            var obj_type = _controller.Type.Where(t => t.typeId.Equals(multi.typeId)).FirstOrDefault();
            var obj_tech = _controller.Technology.Where(t => t.technologyId.Equals(multi.technologyId)).FirstOrDefault();

            if (obj_task != null && obj_type != null && obj_tech != null)
            {
                // I assume that the daily work lasts 6 hours, 2 hours for each subject
                // Standard price is calculated based on 2 months of work, 2 months x 4 tygodnire x 5 dni x 6 godzin
                // 240 h for a project

                int price_daily = obj_task.pricePerHour * 2 + obj_task.pricePerHour * 2 + obj_task.pricePerHour * 2;
                ModelState.AddModelError("", Convert.ToString(price_daily));

            }
            else
            {
                ViewBag.message = "Sorry, we don't have such items.";
            }
            return View(multi);
        }*/

        /*  [HttpGet]
          public IActionResult Services(MultiClass multi)
          {
              if (ModelState.IsValid)
              {
                  var obj_task = _controller.Task.Where(t => t.taskId.Equals(multi.taskId)).FirstOrDefault();
                  var obj_type = _controller.Type.Where(t => t.typeId.Equals(multi.typeId)).FirstOrDefault();
                  var obj_tech = _controller.Technology.Where(t => t.technologyId.Equals(multi.technologyId)).FirstOrDefault();

                  if (obj_task != null && obj_type != null && obj_tech != null)
                  {
                      // I assume that the daily work lasts 6 hours, 2 hours for each subject
                      // Standard price is calculated based on 2 months of work, 2 months x 4 tygodnire x 5 dni x 6 godzin
                      // 240 h for a project

                      int price_daily = obj_task.pricePerHour * 2 + obj_task.pricePerHour * 2 + obj_task.pricePerHour * 2;
                      ModelState.AddModelError("", Convert.ToString(price_daily));

                  }
                  else
                  {
                      ViewBag.message = "Sorry, we don't have such items.";
                  }
              }
              else
              {
                  ModelState.AddModelError("", "An error occured.");
              }
              return View();*/
        //}

    }
}