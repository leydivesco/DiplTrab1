using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassRegistro.Model
{
    public class ClassMateria
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Nombre es requerido"), MinLength(5,ErrorMessage ="Error se aceptan minimo 5 caracteres"),MaxLength(250,ErrorMessage ="Error se aceptan maximo 250 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage ="Codigo es requerido"), MinLength(2, ErrorMessage="Se aceptan minimo 2 caracteres"), MaxLength(7, ErrorMessage ="Se acaptan maximo 7 caracteres")]
        public string Codigo { get; set; }
        [Required(ErrorMessage ="Campo obligatorio, por favor elegir")]
        public Area area { get; set; }
        public bool Disponible { get; set; }
        [Required(ErrorMessage ="Campo requerido"), MinLength(10), MaxLength(1000)]
        public string Objetivos { get; set; }
    }
}
