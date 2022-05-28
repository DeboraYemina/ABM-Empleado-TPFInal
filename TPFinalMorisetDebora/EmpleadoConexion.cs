using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TPFinalMorisetDebora
{
    static class EmpleadoConexion
    {
        static string rutaConexion = "data source=DESKTOP-1VSITPU;initial catalog=empleados_db;integrated security=sspi";
  
        //Trae los datos de bd empleados para cargar en el datagridview de frmprincipal
        internal static object CargarLista()
        {
            SqlConnection conexion = new SqlConnection();
            SqlDataReader lector=null;

            try
            {
                conexion.ConnectionString = rutaConexion;

                SqlCommand comando = new SqlCommand("select * from Empleados", conexion);
                
                conexion.Open();

                List<Empleado> listaEmplados = new List<Empleado>();

                lector = comando.ExecuteReader();
                while(lector.Read())
                {
                    Empleado aux = new Empleado();
                    aux.Id = lector.GetInt32(0);
                    aux.NombreCompleto = lector.GetString(1);
                    aux.DNI = lector.GetString(2);
                    aux.Edad = lector.GetInt32(3);
                    aux.Casado = lector.GetBoolean(4);
                    aux.Salario = Decimal.ToDouble(lector.GetDecimal(5));

                    listaEmplados.Add(aux);
                }

                return listaEmplados;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
                if(lector!=null)
                    lector.Close();
            }
        }

        //Guarda un empleado en db
        internal static void GuardarEmpleado(Empleado aux)
        {
            SqlConnection conexion = new SqlConnection(); 

            try
            {
                conexion.ConnectionString = rutaConexion;

                SqlCommand comando = new SqlCommand("insert into Empleados values(@nombre,@dni,@edad,@casado,@salario)", conexion);
                comando.Parameters.AddWithValue("@nombre", aux.NombreCompleto);
                comando.Parameters.AddWithValue("@dni", aux.DNI);
                comando.Parameters.AddWithValue("@edad", aux.Edad);
                comando.Parameters.AddWithValue("@casado", aux.Casado);
                comando.Parameters.AddWithValue("@salario", aux.Salario);

                conexion.Open();
                comando.ExecuteNonQuery();

            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        //Elimina empleados de db
        internal static void EliminarEmpleado(Empleado aux)
        {
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = rutaConexion;

                SqlCommand comando = new SqlCommand("delete from empleados where id=@id", conexion);
                comando.Parameters.AddWithValue("@id", aux.Id);

                conexion.Open();
                comando.ExecuteNonQuery();

            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        //Modifica empleado de db
        internal static void ModificarEmpleado(Empleado aux)
        {
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = rutaConexion;

                SqlCommand comando = new SqlCommand("UPDATE Empleados SET NombreCompleto = @nombre, DNI = @dni, Edad = @edad, Casado = @casado, Salario = @salario WHERE id = @id", conexion);
                comando.Parameters.AddWithValue("@nombre", aux.NombreCompleto);
                comando.Parameters.AddWithValue("@dni", aux.DNI);
                comando.Parameters.AddWithValue("@edad", aux.Edad);
                comando.Parameters.AddWithValue("@casado", aux.Casado);
                comando.Parameters.AddWithValue("@salario", aux.Salario);
                comando.Parameters.AddWithValue("@id", aux.Id);

                conexion.Open();
                comando.ExecuteNonQuery();

            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                throw ex;

            }
            finally
            {
                conexion.Close();
            }
        }
    }
}
