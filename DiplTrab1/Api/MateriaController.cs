using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassDATA;
using ClassRegistro.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplTrab1.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateria service;
        public MateriaController(IMateria service)
        {
            this.service = service;
        }
        [Route("Materias")]
        public List<ClassMateria> GetMaterias()
        {
            var materias = service.GetMateriaPorNombre("");
            return materias.ToList();
        }
    }
}
