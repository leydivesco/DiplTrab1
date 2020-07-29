using ClassDATA;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplTrab1.Pages
{
    public class CentroMateriaViewComponent : ViewComponent
    {
        private readonly IMateria service;
        public CentroMateriaViewComponent(IMateria service)
        {
            this.service = service;
        }
        public IViewComponentResult Invoke()
        {
            var TotalMateria = service.GetTotalMateriaReg();

            return View("CentroMateria.cshtml",TotalMateria);
        }
    }
}
