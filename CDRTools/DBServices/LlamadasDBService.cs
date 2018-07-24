using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using CDRTools.Models;
using System.IO;

namespace CDRTools.DBServices
{
    public class LlamadasDBService
    {
        private string directorio { get; set; }
        private string err { get; set; }

        public string Llamadas_Carga()
        {
            directorio = ConfigurationManager.AppSettings["directorio"];
            err = "";

            string fecha = DateTime.Now.ToShortDateString().Replace("/", "-");

            foreach (string files in Directory.GetFiles(directorio, "cdr_*.*"))
            {
                var filename = Path.GetFileName(files);

                Llamadas_Escribe(filename);

                if (!string.IsNullOrEmpty(err))
                {
                    break;
                }

                if (!Directory.Exists(directorio + fecha))
                {
                    Directory.CreateDirectory(directorio + fecha);
                }

                File.Move(files, directorio + fecha + @"\\" + filename);
            }

            return err;
        }

        private void Llamadas_Escribe(string filename)
        {
            string connstring = ConfigurationManager.ConnectionStrings["CDRToolsConnection"].ToString();

            using (SqlConnection Connection = new SqlConnection(connstring))
            {
                int linea = 1;

                try
                {
                    Connection.Open();

                    using (SqlTransaction Transaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        using (SqlCommand Command = new SqlCommand("Llamadas_Actualiza", Connection, Transaction))
                        {
                            Command.Parameters.Add("@p_globalCallID_callManagerId", SqlDbType.Int, 4);
                            Command.Parameters.Add("@p_globalCallID_callId", SqlDbType.Int, 4);
                            Command.Parameters.Add("@p_dateTimeOrigination", SqlDbType.Int, 4);
                            Command.Parameters.Add("@p_callingPartyNumber", SqlDbType.VarChar, 50);
                            Command.Parameters.Add("@p_originalCalledPartyNumber", SqlDbType.VarChar, 50);
                            Command.Parameters.Add("@p_duration", SqlDbType.Int, 4);

                            Command.CommandType = CommandType.StoredProcedure;

                            using (var calls = new StreamReader(directorio + filename))
                            {
                                while ((!calls.EndOfStream) && (string.IsNullOrEmpty(err)))
                                {
                                    var call = calls.ReadLine();

                                    call = call.Replace("\"", "");

                                    if (linea < 3)
                                    {
                                        linea++;
                                        continue;
                                    }

                                    var callDetails = call.Split(',');

                                    Command.Parameters["@p_globalCallID_callManagerId"].Value = Convert.ToInt32(callDetails[1]);
                                    Command.Parameters["@p_globalCallID_callId"].Value = Convert.ToInt32(callDetails[2]);
                                    Command.Parameters["@p_dateTimeOrigination"].Value = Convert.ToInt32(callDetails[4]);
                                    Command.Parameters["@p_callingPartyNumber"].Value = callDetails[8];
                                    Command.Parameters["@p_originalCalledPartyNumber"].Value = callDetails[29];
                                    Command.Parameters["@p_duration"].Value = Convert.ToInt32(callDetails[55]);

                                    Command.ExecuteNonQuery();
                                }
                            }
                        }

                        Transaction.Commit();

                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                }
            }
        }

        public Tuple<int,IReadOnlyCollection<Llamada>> Llamadas_Recupera(int? page)
        {
            int totalLlamadas = 0;
            List<Llamada> Llamadas_Registradas = new List<Llamada>();

            string connstring = ConfigurationManager.ConnectionStrings["CDRToolsConnection"].ToString();

            using (SqlConnection Connection = new SqlConnection(connstring))
            {
                try
                {
                    Connection.Open();

                    using (SqlCommand Command = new SqlCommand("Llamadas_Recupera", Connection))
                    {
                        Command.Parameters.Add("@p_page", SqlDbType.Int, 4);
                        Command.Parameters["@p_page"].Value = Convert.ToInt32(page);

                        Command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader DataReader = Command.ExecuteReader())
                        {
                            while (DataReader.Read())
                            {
                                Llamadas_Registradas.Add(new Llamada()
                                {
                                    //globalCallID_callManagerId = (int)DataReader["globalCallID_callManagerId"],
                                    //globalCallID_callId = (int)DataReader["globalCallID_callId"],
                                    dateTimeOrigination = (DateTime)DataReader["dateTimeOrigination"],
                                    callingPartyNumber = DataReader["callingPartyNumber"].ToString(),
                                    originalCalledPartyNumber = DataReader["originalCalledPartyNumber"].ToString(),
                                    duration = (int)DataReader["duration"]
                                });

                                totalLlamadas = (int)DataReader["totalLlamadas"];
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                }
            }

            Tuple<int, IReadOnlyCollection<Llamada>> data = new Tuple<int, IReadOnlyCollection<Llamada>>(totalLlamadas, Llamadas_Registradas.AsReadOnly());

            return data;
        }

    }
}