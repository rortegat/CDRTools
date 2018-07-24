using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CDRTools.Models;

namespace CDRTools.DBServices
{
    public class ExtensionesDBService
    {
        private string connstring;

        private void Connection()
        {
            connstring = ConfigurationManager.ConnectionStrings["CDRToolsConnection"].ToString();
        }

        public List<Extension> Extensiones_Recupera()
        {
            Connection();

            List<Extension> listExtensiones = new List<Extension>();

            using (SqlConnection Conn = new SqlConnection(connstring))
            using (SqlCommand Command = new SqlCommand("Extensiones_Recupera", Conn))
            {
                Conn.Open();

                Command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader DataReader = Command.ExecuteReader())
                {
                    while(DataReader.Read())
                    {
                        listExtensiones.Add(
                            new Extension
                            {
                                Id_Extension = Convert.ToInt32(DataReader["Id_Extension"]),
                                Extension_Descripcion = Convert.ToString(DataReader["Extension_Describe"])
                            });
                    }
                }
            }

            return listExtensiones;
        }
    }
}