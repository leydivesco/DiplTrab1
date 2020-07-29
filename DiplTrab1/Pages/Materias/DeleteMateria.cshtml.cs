using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassDATA;
using ClassRegistro.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiplTrab1.Pages.Materias
{
    public class DeleteMateriaModel : PageModel
    {
        private readonly IMateria service;
        public ClassMateria materia { get; set; }
        public DeleteMateriaModel(IMateria service)
        {
            this.service = service;
        }
        public void OnGet(int Id)
        {
            this.materia = service.GetMateriaPorID(Id);
        }
        public ActionResult Onpost(int Id)
        {
            var materia = service.Delete(Id);
          //  service.Delete(id);
            service.GuardarCambios();
            TempData["MensajeDelete"] = $"Se ha eliminado la materia {materia.Nombre}";
            return RedirectToPage("./MateriasReg");
        }
    }
}