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
    public class EstudiantesRegModel : PageModel
    {
       public IList<ClassEstudiante> Estudiantes { get; set; }
        public IEstudiante Estudiante { get;  }
        [BindProperty(SupportsGet = true)]
        public string Texto { get; set; }
        [TempData]
        public string MensajeDelete { get; set; }
        public EstudiantesRegModel(IEstudiante estudiante)
        {
            Estudiante = estudiante;
        }
        public void OnGet(string Texto)
        {
            this.Estudiantes = Estudiante.GetEstudiantePorNombre(Texto);
        }
    }
}