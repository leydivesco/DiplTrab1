using ClassRegistro.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassDATA
{
    public class InMemoryEstudianteService : IEstudiante
    {
        IList<ClassEstudiante> estudiantes;
        public InMemoryEstudianteService()
        {
            this.estudiantes = new List<ClassEstudiante>() 
            { 
                new ClassEstudiante{ID=1,Matricula="1144105",Nombre="Juan Antonio",Apellido="Castillo",FechaNacimiento=new DateTime(1995,8,16),sexo=Sexo.Masculino},
                new ClassEstudiante{ID=2,Matricula="1144106",Nombre="Juan",Apellido="Castillo",FechaNacimiento=new DateTime(1995,8,16),sexo=Sexo.Masculino},
                new ClassEstudiante{ID=3,Matricula="1144107",Nombre="Antonio",Apellido="Castillo",FechaNacimiento=new DateTime(1995,8,16),sexo=Sexo.Masculino},
                new ClassEstudiante{ID=4,Matricula="1144108",Nombre="Jose",Apellido="Castillo",FechaNacimiento=new DateTime(1995,8,16),sexo=Sexo.Masculino}
            };
        }

        public ClassEstudiante ActualizarEstudiante(ClassEstudiante estudianteactualizado)
        {
            var estudianteActual = estudiantes.SingleOrDefault(d => d.ID == estudianteactualizado.ID);
            estudianteActual.Matricula = estudianteactualizado.Matricula;
            estudianteActual.Nombre = estudianteactualizado.Nombre;
            estudianteActual.Apellido = estudianteactualizado.Apellido;
            estudianteActual.FechaNacimiento = estudianteactualizado.FechaNacimiento;
            estudianteActual.sexo = estudianteactualizado.sexo;
            return estudianteActual;
        }

        public ClassEstudiante Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ClassEstudiante GetEstudiantePorId(int id)
        {
            return this.estudiantes.SingleOrDefault(d => d.ID == id);
        }

        public IList<ClassEstudiante> GetEstudiantePorNombre(string Texto)
        {
            if (!string.IsNullOrEmpty(Texto))
            {
                Texto = Texto.ToLower();
            }
            return estudiantes.Where(m=>string.IsNullOrEmpty(Texto)||m.NombreCompleto.ToLower().Contains(Texto)).OrderBy(m => m.ID).ToList();
        }

        public int GetTotalEstudianteReg()
        {
            throw new NotImplementedException();
        }

        public int GuardarCambios()
        {
            return 1;
        }

        public ClassEstudiante NuevoEstudiante(ClassEstudiante nuevoEst)
        {
            nuevoEst.ID = estudiantes.Max(m => m.ID) + 1;
            estudiantes.Add(nuevoEst);
            return nuevoEst;
        }
    }
}
