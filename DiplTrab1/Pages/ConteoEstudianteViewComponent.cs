using ClassDATA;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplTrab1.Pages
{
    public class ConteoEstudianteViewComponent : ViewComponent
    {
        private readonly IEstudiante service;
        public ConteoEstudianteViewComponent(IEstudiante service)
        {
            this.service = service;
        }
        public IViewComponentResult Invoke()
        {
            var TotalEstudiante = service.GetTotalEstudianteReg();
            return View("ConteoEstudiante.cshtml", TotalEstudiante);
        }
    }
}
