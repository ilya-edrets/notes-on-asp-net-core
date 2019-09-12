using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Index()
        {
            var model = new TasksViewModel
            {
                User = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Name).Value
            };

            return View(model);
        }
    }
}