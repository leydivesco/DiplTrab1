using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassDATA;
using ClassRegistro.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiplTrab1.Pages.Estudiante
{
    public class DetalleEstudianteModel : PageModel
    {
        public readonly IEstudiante est;
        public ClassEstudiante Estudiante { get; set; }
        [TempData]
        public string Mensaje { get; set; }
        public DetalleEstudianteModel(IEstudiante est)
        {
            this.est = est;
        }
        public ActionResult OnGet(int id)
        {
            Estudiante = est.GetEstudiantePorId(id);
            if (Estudiante == null)
            {
                return RedirectToPage("./ErrorEstudiante");
            }
            else
            {
                return Page();
            }
        }
    }
}