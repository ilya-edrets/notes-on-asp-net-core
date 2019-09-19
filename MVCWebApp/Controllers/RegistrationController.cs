using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers
{
    public class RegistrationController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string login, string password)
        {
            var user = DataAccess.Models.User.Find(login);

            if(user != null) {
                return View(new RegistrationViewModel { RegistrationError = $"User with name {login} already exist."});
            }

            user = new DataAccess.Models.User();
            user.Login = login;
            user.Password = password;
            user.Insert();

            return View("Congratulations");
        }
    }
}