using ClassRegistro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDATA
{
    public class ClassRegistroEstService : IEstudiante
    {
        private ClassRegistroContext db;
        public ClassRegistroEstService(ClassRegistroContext db)
        {
            this.db = db;
        }
        public ClassEstudiante ActualizarEstudiante(ClassEstudiante estudianteactualizado)
        {
            var estudianteActual = db.Estudiante.SingleOrDefault(d => d.ID == estudianteactualizado.ID);

            estudianteActual.Matricula = estudianteactualizado.Matricula;
            estudianteActual.Nombre = estudianteactualizado.Nombre;
            estudianteActual.Apellido = estudianteactualizado.Apellido;
            estudianteActual.FechaNacimiento = estudianteactualizado.FechaNacimiento;
            estudianteActual.sexo = estudianteactualizado.sexo;
            return estudianteActual;
        }

        public ClassEstudiante Delete(int id)
        {
            var estudiante = db.Estudiante.Single(m => m.ID == id);
            db.Estudiante.Remove(estudiante);
            return estudiante;
        }

        public ClassEstudiante GetEstudiantePorId(int id)
        {
            return this.db.Estudiante.SingleOrDefault(d => d.ID == id);
        }

        public IList<ClassEstudiante> GetEstudiantePorNombre(string Texto)
        {
            if (!string.IsNullOrEmpty(Texto))
            {
                Texto = Texto.ToLower();
            }
            return db.Estudiante.Where(m => string.IsNullOrEmpty(Texto) || m.NombreCompleto.ToLower().Contains(Texto)).OrderBy(m => m.ID).ToList();
        }

        public int GetTotalEstudianteReg()
        {
            return db.Estudiante.Count();
        }

        public int GuardarCambios()
        {
            return db.SaveChanges();
        }

        public ClassEstudiante NuevoEstudiante(ClassEstudiante nuevoEst)
        {
            db.Estudiante.Add(nuevoEst);
            return nuevoEst;
        }
    }
}
