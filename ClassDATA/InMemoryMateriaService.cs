using ClassRegistro.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassDATA
{
    public class InMemoryMateriaService : IMateria
    {
        IList<ClassMateria> materias;
        public InMemoryMateriaService()
        {
            this.materias = new List<ClassMateria>()
            { 
                new ClassMateria{ID=1,Codigo="01",area=Area.Informatica,Disponible=true,Nombre="Introduccion a la programacion",Objetivos="enseñar lo basico de la programacion"},
                new ClassMateria{ID=2,Codigo="02",area=Area.Ingenieria,Disponible=true,Nombre="Introduccion a la Ingenieria en Informatica",Objetivos="explicar las funciones de un ingeniero"},
                new ClassMateria{ID=3,Codigo="03",area=Area.Informatica,Disponible=true,Nombre="Programacion 1",Objetivos="enseñar lo basico de la programacion"},
                new ClassMateria{ID=4,Codigo="04",area=Area.Informatica,Disponible=true,Nombre="Programacion 2",Objetivos="enseñar lo basico de la programacion"}
            };
        }

        public ClassMateria ActualizarMateria(ClassMateria materiaactualizada)
        {
            var materiaActual = materias.SingleOrDefault(m => m.ID == materiaactualizada.ID);
            materiaActual.Nombre = materiaactualizada.Nombre;
            materiaActual.Codigo = materiaactualizada.Codigo;
            materiaActual.Objetivos = materiaactualizada.Objetivos;
            materiaActual.Disponible = materiaactualizada.Disponible;
            materiaActual.area = materiaactualizada.area;

            return materiaActual;
        }

        public ClassMateria Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ClassMateria GetMateriaPorID(int Id)
        {
           return this.materias.SingleOrDefault(d => d.ID == Id);
        }
        public IList<ClassMateria> GetMateriaPorNombre(string Texto)
        {
            if (!string.IsNullOrEmpty(Texto))
            {
                Texto = Texto.ToLower();
            }
            
            return materias.Where(m=>string.IsNullOrEmpty(Texto)|| m.Nombre.ToLower().Contains(Texto)).OrderBy(m => m.ID).ToList();
        }

        public int GetTotalMateriaReg()
        {
            return materias.Count();
        }

        public int GuardarCambios()
        {
            return 1;
        }

        public ClassMateria NuevaMateria(ClassMateria nuevo)
        {
            nuevo.ID = materias.Max(m => m.ID ) + 1;
            materias.Add(nuevo);
            return nuevo;
        }
    }
}