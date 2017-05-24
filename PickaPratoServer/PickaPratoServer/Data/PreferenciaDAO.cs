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
                where Preferencias.cliente = @username
            ";
            command.Parameters.Add(new SqlParameter("@username", id));
            var result = command.ExecuteReader();
            while (result.Read())
            {
                res.Add((String)result["ingrediente"]);
            }
            connection.Close();
            return res;
        }

        public void Put(List<String> inserir){
            string user = inserir[0];
            connection.Open();
            //Remover preferencias
            SqlCommand command = connection.CreateCommand();
            command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
            DELETE FROM [dbo].[Preferencias]
            WHERE cliente = @username
            ";
            command.Parameters.Add(new SqlParameter("@username", user));
            var result = command.ExecuteNonQuery();


            //Insere preferencias
            for (int i =1; i< inserir.Count; i++) {
                command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = @"
                INSERT INTO [dbo].[Preferencias]
                ([cliente],[ingrediente])
                VALUES(@user,@ingrediente)
                ";
                command.Parameters.Add(new SqlParameter("@user", user));
                command.Parameters.Add(new SqlParameter("@ingrediente", inserir[i]));
                result = command.ExecuteNonQuery();
            }
        }
    }
}