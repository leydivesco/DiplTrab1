using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassDATA;
using ClassRegistro.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplTrab1.Pages.Estudiante
{
    public class EditarEstudianteModel : PageModel
    {
        private readonly IEstudiante estudianteservice;
        private readonly IHtmlHelper helper2;
        [BindProperty]
        public ClassEstudiante Estudiante { get; set; }
        public IEnumerable<SelectListItem> Genero { get; set; }

        public EditarEstudianteModel(IEstudiante estudianteservice, IHtmlHelper helper2)
        {
            this.helper2 = helper2;
            this.estudianteservice = estudianteservice;
            
        }       
        public void OnGet(int? id)
        {
            Genero = helper2.GetEnumSelectList<Sexo>();
            if (id.HasValue)
            {
                Estudiante = estudianteservice.GetEstudiantePorId(id.Value);
            }
            else
            {
                Estudiante = new ClassEstudiante();
            }
            
        }
        public ActionResult OnPost()
        {
            Genero = helper2.GetEnumSelectList<Sexo>();
            if (ModelState.IsValid)
            {
                if (Estudiante.ID == 0)
                {
                    Estudiante = estudianteservice.NuevoEstudiante(Estudiante);
                    TempData["Mensaje"] = "Registro Creado";
                }
                else
                {
                    Estudiante = estudianteservice.ActualizarEstudiante(Estudiante);
                    TempData["Mensaje"] = "Datos Actualizado";
                }
                estudianteservice.GuardarCambios();
                return RedirectToPage("./DetalleEstudiante", new { id=Estudiante.ID});
            }
            return Page();
        }
    }
}