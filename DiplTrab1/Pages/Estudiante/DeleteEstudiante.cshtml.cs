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
    public class DeleteEstudianteModel : PageModel
    {
        private readonly IEstudiante service;
        public ClassEstudiante estudiante { get; set; }

        public DeleteEstudianteModel(IEstudiante service)
        {
            this.service = service;
        }
        public void OnGet(int id)
        {
            this.estudiante = service.GetEstudiantePorId(id);
        }
        public ActionResult OnPost(int id)
        {
           var estudiante= service.Delete(id);
            service.GuardarCambios();
            TempData["MensajeDelete"] = $"Se ha eliminado el estudiante {estudiante.NombreCompleto}";
            return RedirectToPage("./EstudiantesReg");
        }
    }
}