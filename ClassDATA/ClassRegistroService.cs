using ClassRegistro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDATA
{
    public class ClassRegistroService : IMateria
    {
        private ClassRegistroContext db;
        public ClassRegistroService(ClassRegistroContext db)
        {
            this.db = db;
        }
        public ClassMateria ActualizarMateria(ClassMateria materiaactualizada)
        {
            var materiaActual = db.Materias.SingleOrDefault(m => m.ID == materiaactualizada.ID);

            materiaActual.Nombre = materiaactualizada.Nombre;
            materiaActual.Codigo = materiaactualizada.Codigo;
            materiaActual.Objetivos = materiaactualizada.Objetivos;
            materiaActual.Disponible = materiaactualizada.Disponible;
            materiaActual.area = materiaactualizada.area;

            return materiaActual;
        }

        public ClassMateria Delete(int id)
        {
            var materia = db.Materias.Single(m => m.ID == id);
            db.Materias.Remove(materia);

            return materia;
        }

        public ClassMateria GetMateriaPorID(int Id)
        {
            return this.db.Materias.SingleOrDefault(d => d.ID == Id);
        }

        public IList<ClassMateria> GetMateriaPorNombre(string Texto)
        {

            if (!string.IsNullOrEmpty(Texto))
            {
                Texto = Texto.ToLower();
            }

            return db.Materias.Where(m => string.IsNullOrEmpty(Texto) || m.Nombre.ToLower().Contains(Texto)).OrderBy(m => m.ID).ToList();

        }

        public int GetTotalMateriaReg()
        {
            return db.Materias.Count();
        }

        public int GuardarCambios()
        {
            return db.SaveChanges();
        }

        public ClassMateria NuevaMateria(ClassMateria nuevo)
        {
            db.Materias.Add(nuevo);
            return nuevo;
        }
    }
}
