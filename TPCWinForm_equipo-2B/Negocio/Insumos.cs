using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Insumos
    {
        public List<Insumo> listarInsumos()
        {
            List<Insumo> lista = new List<Insumo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //Consulta a la DB ¬
                datos.setConsulta("SELECT i.IdInsumo, i.Nombre, i.Precio, i.Stock FROM Insumos AS i;;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Insumo ins = new Insumo();

                   ins.ID = datos.Lector["ID"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["ID"]);
                    ins.Nombre = datos.Lector["Nombre"] is DBNull ? "no especificado" : Convert.ToString(datos.Lector["Nombre"]);
                    ins.Precio = datos.Lector["Precio"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["Precio"]);
                    ins.Stock = datos.Lector["Stock"] is DBNull ? 0 : Convert.ToInt32(datos.Lector["Stock"]);
                    lista.Add(ins);
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
