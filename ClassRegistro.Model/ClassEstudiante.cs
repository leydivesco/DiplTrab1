using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClassRegistro.Model
{
    public class ClassEstudiante
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Campo Obligatorio"),MinLength(4),MaxLength(7)]
        public string Matricula { get; set; }
        [Required(ErrorMessage = "Nombre es requerido"), MinLength(5, ErrorMessage = "Error se aceptan minimo 5 caracteres"), MaxLength(250, ErrorMessage = "Error se aceptan maximo 250 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Apellido es requerido"), MinLength(5, ErrorMessage = "Error se aceptan minimo 5 caracteres"), MaxLength(250, ErrorMessage = "Error se aceptan maximo 250 caracteres")]
        public string Apellido { get; set; }
        public string NombreCompleto { get { return Nombre + "" + Apellido; } }
        [Required(ErrorMessage = "Campo obligatorio, por favor elegir fecha")]
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get
            {
                _ = DateTime.Today;
                int edad = DateTime.Today.Year - FechaNacimiento.Year;
                if (DateTime.Today < FechaNacimiento.AddYears(edad))
                    return --edad;
                else
                    return edad;
            } }
        public Sexo sexo { get; set; }
    }
}
