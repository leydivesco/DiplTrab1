using ClassRegistro.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;

namespace ClassDATA
{
    public class ClassRegistroEstServiceAdo : IEstudiante
    {
        private readonly string connectionString;
        private int rowAffect;
        List<ClassEstudiante> ActEstudiante;
        List<ClassEstudiante> crearEstudiante;
        public ClassRegistroEstServiceAdo(string connectionString)
        {
            this.connectionString = connectionString;
            ActEstudiante = new List<ClassEstudiante>();
            crearEstudiante = new List<ClassEstudiante>();
        }
        public ClassEstudiante ActualizarEstudiante(ClassEstudiante estudianteactualizado)
        {
            ActEstudiante.Add(estudianteactualizado);
            return estudianteactualizado;
        }

        public ClassEstudiante GetEstudiantePorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var query = @"select ID, Matricula,Nombre,Apellido, FechaNacimiento, sexo
                              FROM Estudiantes
                              WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                var datareader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                ClassEstudiante estudiante = null;
                while (datareader.Read())
                {
                    estudiante = new ClassEstudiante
                    {
                        ID = Convert.ToInt32(datareader["ID"]),
                        Matricula = datareader["Matricula"].ToString(),
                        Nombre = datareader["Nombre"].ToString(),
                        Apellido = datareader["Apellido"].ToString(),
                        FechaNacimiento=Convert.ToDateTime(datareader["FechaNacimiento"]),
                        sexo = (Sexo)datareader["Sexo"]                       
                    };
                }
                return estudiante;
            }
        }

        public IList<ClassEstudiante> GetEstudiantePorNombre(string Texto)
        {
            //Conexion
            SqlConnection conn = new SqlConnection(this.connectionString);

            //SENTENCIA SELECT
            var query = @"Select *
                            from Estudiante
                            where Nombre like '%' + @text + '%' or isnull(@text,'')= ''";


            //BUSQUEDA DE DATOS
            SqlCommand cmd = new SqlCommand(query, conn);

            //
            SqlParameter text = new SqlParameter("@text", Texto);

            if (Texto == null)
            {
                text.Value = DBNull.Value;
            }

            text.SqlDbType = System.Data.SqlDbType.VarChar;

            //parametro a utilizar
            cmd.Parameters.Add(text);

            //ejecutar, abrir conexion
            conn.Open();

            //traer datos
            var datareader = cmd.ExecuteReader();

            //traer lista
            List<ClassEstudiante> estudiantes = new List<ClassEstudiante>();

            //read atrae cada fila de datos
            while (datareader.Read())
            {
                estudiantes.Add(new ClassEstudiante
                {
                    ID = Convert.ToInt32(datareader["ID"]),
                    Matricula=datareader["Matricula"].ToString(),
                    Nombre = datareader["Nombre"].ToString(),
                    Apellido=datareader["Apellido"].ToString(),
                    FechaNacimiento=Convert.ToDateTime(datareader["FechaNacimiento"]),
                   sexo=(Sexo)datareader["Sexo"]
                });
            }

            //cerrar conexion
            conn.Close();
            conn.Dispose();
            return estudiantes;
        }

        public int GuardarCambios()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                  using (TransactionScope scope = new TransactionScope())
                  {
                        foreach (var estudiantesAct in ActEstudiante)
                        {
                            ProcesarActualizacion(estudiantesAct, conn);
                        }
                        foreach (var estudiantes in crearEstudiante)
                        {
                            NewEstudiante(estudiantes, conn);
                        }
                        scope.Complete();
                  }                
            }
            return rowAffect;
        }

        private void NewEstudiante(ClassEstudiante estudiantes, SqlConnection conn)
        {
            var query = "dbo.InsertarEstudiantes";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Matricula", estudiantes.Matricula);
            cmd.Parameters.AddWithValue("@Nombre", estudiantes.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", estudiantes.Apellido);
            cmd.Parameters.AddWithValue("@FechaNacimiento", estudiantes.FechaNacimiento);
            cmd.Parameters.AddWithValue("@sexo", estudiantes.sexo);

            conn.Open();
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            estudiantes.ID = id;
            rowAffect++;
        }

        private void ProcesarActualizacion(ClassEstudiante estudiantesAct, SqlConnection conn)
        {
            var query = @"update Estudiantes
                              set Matriula=@Matricula,
                               Nombre=@Nombre,
                               Apellido=@Apellido,
                               FechaNacimiento = @FechaNacimiento,
                               sexo=@sexo
                              where ID=@id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", estudiantesAct.ID);
            cmd.Parameters.AddWithValue("@Matricula", estudiantesAct.Matricula);
            cmd.Parameters.AddWithValue("@Nombre", estudiantesAct.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", estudiantesAct.Apellido);
            cmd.Parameters.AddWithValue("@FechaNacimiento", estudiantesAct.FechaNacimiento);
            cmd.Parameters.AddWithValue("@sexo", estudiantesAct.sexo);

            conn.Open();
            rowAffect += cmd.ExecuteNonQuery();
            conn.Close();
        }

        public ClassEstudiante NuevoEstudiante(ClassEstudiante nuevoEst)
        {
            crearEstudiante.Add(nuevoEst);
            return nuevoEst;
        }

        public ClassEstudiante Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int GetTotalEstudianteReg()
        {
            throw new NotImplementedException();
        }
    }
}
