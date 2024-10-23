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
                datos.setConsulta("select m.NumeroMesa from Mesas as m;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Mesa mes = new Mesa();

                    mes.NumeroMesa = datos.Lector["NumeroMesa"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["NumeroMesa"]);
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
