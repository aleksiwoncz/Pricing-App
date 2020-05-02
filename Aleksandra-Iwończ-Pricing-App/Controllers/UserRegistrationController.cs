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
                    return RedirectToAction("LoggedIn");
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

    }
}