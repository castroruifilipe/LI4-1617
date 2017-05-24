using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PickaPratoServer.Data
{
    public class IngredienteDAO {

        SqlConnection connection = new SqlConnection("Server=DIOGO-PC\\SQLEXPRESS; Database=PickPrato; Trusted_Connection=True;");

        public List<String> Get() {
            List<String> lista = new List<string>();
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
            select *
            from Ingrediente
        ";
            var result = command.ExecuteReader();
            while (result.Read()) {
                lista.Add((String)result["designacao"]);
            }
            return lista;
        }
    }
}