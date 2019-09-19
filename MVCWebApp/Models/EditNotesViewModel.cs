using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models
{
    public class EditNotesViewModel
    {
        public List<Note> Notes { get; set; }

        public Note NoteForEdit { get; set; }
    }
}
