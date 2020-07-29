using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassRegistro.Model.Cafeteria
{
   public class ClassCategoria
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public List<ClassProducto> Productos { get; set; }
    }
}
