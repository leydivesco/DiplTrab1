using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassDATA;
using DiplTrab1.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DiplTrab1.Pages.Cafeteria.Categoria
{
    public class ListCategoriaModel : PageModel
    {
        private readonly ClassRegistroContext db;
        public List<ClassCategoriaDto> CategoriaDtos { get; set; }
        public ListCategoriaModel(ClassRegistroContext db)
        {
            this.db = db;
        }
        public void OnGet()
        {
            var Categoria = db.Categorias.Include(c => c.Productos).ToList();

            this.CategoriaDtos = Categoria.Select(c => new ClassCategoriaDto
            {
                ID = c.ID,
                Nombre = c.Nombre,
                CantidadProductos= c.Productos.Count
            }).ToList();
        }
    }
}