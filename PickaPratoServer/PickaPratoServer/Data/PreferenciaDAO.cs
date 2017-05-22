using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PickaPratoServer.Data
{
    public class PreferenciaDAO
    {
        SqlConnection connection = new SqlConnection("Server=DIOGO-PC\\SQLEXPRESS; Database=PickPrato; Trusted_Connection=True;");

        public List<String> Get(String id)
        {
            List<string> res = new List<string>();
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                select * from Preferencias
                join Ingrediente
                on Ingrediente.idIngrediente = Preferencias.ingrediente
                where Preferencias.cliente = @username
            ";
            command.Parameters.Add(new SqlParameter("@username", id));
            var result = command.ExecuteReader();
            while (result.Read())
            {
                res.Add((String)result["designacao"]);
            }
            connection.Close();
            return res;
        }
    }
}