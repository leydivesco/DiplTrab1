using ClassRegistro.Model;
using System.Collections.Generic;
using System.Text;

namespace ClassDATA
{

    public interface IMateria
    {
        IList<ClassMateria> GetMateriaPorNombre(string Texto);
        public ClassMateria GetMateriaPorID(int Id);
        ClassMateria ActualizarMateria(ClassMateria materiaactualizada);
        ClassMateria NuevaMateria(ClassMateria nuevo);
        int GuardarCambios();
        ClassMateria Delete(int id);
        int GetTotalMateriaReg();
    }
}