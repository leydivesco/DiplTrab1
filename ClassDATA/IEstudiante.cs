using ClassRegistro.Model;
using System.Collections.Generic;
using System.Text;

namespace ClassDATA
{
    public interface IEstudiante
    {
        IList<ClassEstudiante> GetEstudiantePorNombre(string Texto);
        public ClassEstudiante GetEstudiantePorId(int id);
        ClassEstudiante ActualizarEstudiante(ClassEstudiante estudianteactualizado);
        ClassEstudiante NuevoEstudiante(ClassEstudiante nuevoEst);
        int GuardarCambios();
        ClassEstudiante Delete(int id);
        int GetTotalEstudianteReg();
    }
}
