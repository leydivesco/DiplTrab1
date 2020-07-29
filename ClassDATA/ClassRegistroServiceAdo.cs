using ClassRegistro.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Transactions;

namespace ClassDATA
{
    public class ClassRegistroServiceAdo : IMateria
    {
        private readonly string connectionString;
        private int rowAffect;
        List<ClassMateria> materiasActualizar;
        List<ClassMateria> crearmateria;
        public ClassRegistroServiceAdo(string connectionString)
        {
            this.connectionString = connectionString;
            materiasActualizar = new List<ClassMateria>();
            crearmateria = new List<ClassMateria>();
        }
        public ClassMateria ActualizarMateria(ClassMateria materiaactualizada)
        {
            materiasActualizar.Add(materiaactualizada);

            return materiaactualizada;
        }

        public ClassMateria GetMateriaPorID(int Id)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                var query = @"select ID, Nombre,Codigo,area, Disponible, Objetivos
                              FROM Materias
                              WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@id", Id);
                conn.Open();
                var datareader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                ClassMateria materias = null;
                while (datareader.Read())
                {
                    materias= new ClassMateria
                    {
                        ID = Convert.ToInt32(datareader["ID"]),
                        Nombre = datareader["Nombre"].ToString(),
                        Codigo = datareader["Codigo"].ToString(),
                        area = (Area)datareader["Area"],
                        Objetivos = datareader["Objetivos"].ToString()
                    };
                }
                return materias;                
            }
        }

        public IList<ClassMateria> GetMateriaPorNombre(string Texto)
        {
            //Conexion
            using(SqlConnection conn = new SqlConnection(this.connectionString))
            {
                //SENTENCIA SELECT
                var query = @"Select *
                            from Materias
                            where Nombre like '%' + @text + '%' or isnull(@text,'')= ''";


                //BUSQUEDA DE DATOS
                SqlCommand cmd = new SqlCommand(query, conn);                
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
                List<ClassMateria> materias = new List<ClassMateria>();

                //read atrae cada fila de datos
                while (datareader.Read())
                {
                    materias.Add(new ClassMateria
                    {
                        ID = Convert.ToInt32(datareader["ID"]),
                        Nombre = datareader["Nombre"].ToString(),
                        Codigo = datareader["Codigo"].ToString(),
                        area = (Area)datareader["area"],
                        Objetivos = datareader["Objetivos"].ToString()
                    });
                }
                //cerrar conexion
                conn.Close();
                return materias;
            }
        }

        public int GuardarCambios()
        {
            using (SqlConnection conn= new SqlConnection(connectionString))
            {
                using(TransactionScope scope = new TransactionScope())
                { 
                    foreach (var materia in materiasActualizar)
                    {
                        ProcesarActualizacion(materia, conn);
                    }
                    foreach (var materia in crearmateria)
                    {
                        ProcesaraCrear(materia, conn);
                    }
                    scope.Complete();
                }
            }
            return rowAffect;
        }

        private void ProcesaraCrear(ClassMateria nuevo, SqlConnection conn)
        {
           
            var query = "dbo.InsertarMaterias";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //SqlParameter prm = new SqlParameter("@id", System.Data.SqlDbType.Int);
                //prm.Direction = System.Data.ParameterDirection.Output;

                //cmd.Parameters.Add(prm);
                cmd.Parameters.AddWithValue("@nombre", nuevo.Nombre);
                cmd.Parameters.AddWithValue("@codigo", nuevo.Codigo);
                cmd.Parameters.AddWithValue("@area", nuevo.area);
                cmd.Parameters.AddWithValue("@disponible", nuevo.Disponible);
                cmd.Parameters.AddWithValue("@Objetivos", nuevo.Objetivos);

                conn.Open();
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

            //nuevo.ID = Convert.ToInt32(prm.Value);
            nuevo.ID = id;
            rowAffect ++;
        }

        private void ProcesarActualizacion(ClassMateria materiaactualizada, SqlConnection conn)
        {           
                var query = @"update Materias
                              set Nombre=@nombre,
                               Codigo=@codigo,
                               area = @area,
                               Disponible=@disponible,
                               Objetivos=@Objetivos
                              where ID=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", materiaactualizada.ID);
                cmd.Parameters.AddWithValue("@nombre", materiaactualizada.Nombre);
                cmd.Parameters.AddWithValue("@codigo", materiaactualizada.Codigo);
                cmd.Parameters.AddWithValue("@area", materiaactualizada.area);
                cmd.Parameters.AddWithValue("@disponible", materiaactualizada.Disponible);
                cmd.Parameters.AddWithValue("@Objetivos", materiaactualizada.Objetivos);

                conn.Open();
                rowAffect += cmd.ExecuteNonQuery();
                conn.Close();
                
            
        }

        public ClassMateria NuevaMateria(ClassMateria nuevo)
        {
            crearmateria.Add(nuevo);
            return nuevo;         
        }

        public ClassMateria Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int GetTotalMateriaReg()
        {
            throw new NotImplementedException();
        }
    }
}
