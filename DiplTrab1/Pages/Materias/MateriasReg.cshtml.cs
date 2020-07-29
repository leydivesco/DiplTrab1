using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassDATA;
using ClassRegistro.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace DiplTrab1.Pages.Materias
{
    public class MateriasRegModel : PageModel
    {
        public string Mensaje { get; set; }
        private IConfiguration configuration;
        public IList<ClassMateria> Materias { get; set; }
        public IMateria Materiaservice { get; }
        
        [BindProperty(SupportsGet =true)]
        public string Texto { get; set; }
        [TempData]
        public string MensajeDelete { get; set; }
        public MateriasRegModel(IConfiguration config,IMateria materiaservice)
        {
            this.configuration = config;
            Materiaservice = materiaservice;
        }
        public void OnGet(string Texto)
        {
            this.Mensaje = configuration["Mensaje"];
            this.Materias = Materiaservice.GetMateriaPorNombre(Texto);
        }
    }
}