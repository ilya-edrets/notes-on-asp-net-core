﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using DataAccess.Models;

namespace MVCWebApp.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        public IActionResult Index()
        {
            var notes = Note.GetAllByUserId(this.GetCurrentUser().Id);

            var model = new NotesViewModel();
            model.Notes = notes;

            return View(model);
        }

        public IActionResult Add(string value)
        {
            var note = new Note(this.GetCurrentUser().Id);
            note.Text = value;
            note.Insert();

            return this.RedirectToAction("Index");
        }

        private User GetCurrentUser()
        {
            var login = HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (login == null)
            {
                return null;
            }

            return DataAccess.Models.User.Find(login);
        }
    }
}