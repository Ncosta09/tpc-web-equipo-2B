using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Negocio
{
     public class MesasSalon
    {
        public List<Mesa> listarMesa()
        {
            List<Mesa> lista = new List<Mesa>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //Consulta a la DB ¬
                datos.setConsulta("select m.NumeroMesa, m.Estado from Mesas as m;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesa mes = new Mesa();

                    mes.NumeroMesa = datos.Lector["NumeroMesa"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["NumeroMesa"]);
                    mes.Estado = datos.Lector["Estado"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["Estado"]);
                    lista.Add(mes);
                }


                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void ActualizarEstadoMesaUno(int numeroMesa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Actualizar el estado de la mesa a 1 (ocupada)
                datos.setConsulta("UPDATE Mesas SET Estado = 1 WHERE NumeroMesa = @NumeroMesa;");
                datos.setParametro("@NumeroMesa", numeroMesa);
                datos.ejecutarAccion(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void ActualizarEstadoMesaCero(int numeroMesa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Actualizar el estado de la mesa a 1 (ocupada)
                datos.setConsulta("UPDATE Mesas SET Estado = 0 WHERE NumeroMesa = @NumeroMesa;");
                datos.setParametro("@NumeroMesa", numeroMesa);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int ObtenerEstadoMesa(int numeroMesa)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT Estado FROM Mesas WHERE NumeroMesa = @NumeroMesa;");
                datos.setParametro("@NumeroMesa", numeroMesa);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return datos.Lector["Estado"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["Estado"]);
                }
                else
                {
                    throw new Exception("Mesa no encontrada.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Mesa> listarMesasMesero(int IdUsuario)
        {
            List<Mesa> lista = new List<Mesa>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //Consulta a la DB ¬
                datos.setConsulta("select m.NumeroMesa, m.Estado from Mesas as m inner join UsuariosxMesa as U on u.IdMesa = m.IdMesa where m.Estado = 1 and u.IdUsuario = @IdUsuario;");
                datos.setParametro("@IdUsuario", IdUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesa mes = new Mesa();

                    mes.NumeroMesa = datos.Lector["NumeroMesa"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["NumeroMesa"]);
                    mes.Estado = datos.Lector["Estado"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["Estado"]);
                    lista.Add(mes);
                }


                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
