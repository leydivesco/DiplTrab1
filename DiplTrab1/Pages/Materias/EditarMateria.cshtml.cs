using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassDATA;
using ClassRegistro.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiplTrab1.Pages.Materias
{
    public class EditarMateriaModel : PageModel
    {
        
        private readonly IMateria service;
        private readonly IHtmlHelper helper;
        [BindProperty]
        public ClassMateria Materia { get; set; }
        public IEnumerable<SelectListItem> Areas { get; set; }
        public EditarMateriaModel(IMateria service, IHtmlHelper helper)
        {
            this.helper = helper;
            this.service = service;
        }
        public void OnGet(int? id)
        {
            Areas = helper.GetEnumSelectList<Area>();
            if (id.HasValue)
            {
                Materia = service.GetMateriaPorID(id.Value);
            }
            else
            {
                Materia = new ClassMateria();
            }
           
        }
        public ActionResult OnPost()
        {
            Areas = helper.GetEnumSelectList<Area>();

            if (ModelState.IsValid)
            {
                if (Materia.ID == 0)
                {
                    Materia = service.NuevaMateria(Materia);
                    TempData["Mensaje"] = "Registro Creado";
                }
                else
                {
                    Materia = service.ActualizarMateria(Materia);
                    TempData["Mensaje"] = "Datos Actualizados";
                }
                service.GuardarCambios();
                return RedirectToPage("./DetalleMateria",new { id=Materia.ID});
            }
            return Page();
        }
    }
}