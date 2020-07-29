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
    public class DetalleMateriaModel : PageModel
    {
        public readonly IMateria service;
        public ClassMateria materia { get; set; }
        [TempData]
        public string Mensaje { get; set; }
        public DetalleMateriaModel(IMateria service)
        {
            this.service = service;
        }
        public ActionResult OnGet(int Id)
        {
            materia = service.GetMateriaPorID(Id);
            //if (TempData["Mensaje"] != null)
            //{
            //    Mensaje = TempData["Mensaje"].ToString();
            //}
            

            if (materia==null)
            {
                return RedirectToPage("ErrorMateria");
            }
            else
            {
                return Page();
            }
        }
    }
}