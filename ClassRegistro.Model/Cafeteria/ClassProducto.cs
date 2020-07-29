using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRegistro.Model.Cafeteria
{
  public class ClassProducto
    {
        public int ID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int CategoriaId { get; set; }
        public ClassCategoria Categoria { get; set; }
    }
}
