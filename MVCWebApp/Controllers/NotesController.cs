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

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult Add(string value)
        {
            var note = new Note(this.GetCurrentUser().Id);
            note.Text = value;
            note.Insert();

            return this.Index();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var notes = Note.GetAllByUserId(this.GetCurrentUser().Id);

            var model = new EditNotesViewModel();
            model.Notes = notes;
            model.NoteForEdit = notes.Single(x => x.Id == id);

            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Update(Guid id, string value)
        {
            var note = Note.Find(id);
            note.Text = value;
            note.Update();

            return this.Index();
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            Note.Find(id).Delete();

            return this.Index();
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