using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace MVCWebApp.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        public IActionResult Index()
        {
            var model = new TasksViewModel();

            return View(model);
        }

        private string GetCurrentUserLogin()
        {
            return HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        }
    }
}