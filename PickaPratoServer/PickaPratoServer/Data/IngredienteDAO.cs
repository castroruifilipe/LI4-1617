using PickaPratoServer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Diagnostics;

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
            connection.Close();
            return lista;
        }

        public void Put (String ing)
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                INSERT INTO Ingrediente
                ([designacao])
                VALUES
                (@ingrediente)
                ";
            command.Parameters.Add(new SqlParameter("@ingrediente", ing));
            command.ExecuteNonQuery();

            connection.Close();
        }

        public bool TestaIngredientes(int idPrato,List<String> prefs)
        {
            bool r = true;
            List<Ingrediente> lista = new List<Ingrediente>();
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
            select *
            from Prato_possui_Ingrediente
            where prato=@id
            ";
            command.Parameters.Add(new SqlParameter("@id", idPrato));
            var result = command.ExecuteReader();
            while (result.Read())
            {
                string designacao = (String)result["ingrediente"];
                if (prefs.Contains(designacao))
                {
                    byte Customizavel = (byte)result["customizavel"];
                    if (Customizavel==0)
                    {
                        r = false;
                        break;
                    }
                }
            }
            connection.Close();
            return r;
        }

        public bool PesquisaPratoNoIng(int idPrato, String ing)
        {
            bool r = true;
            List<Ingrediente> lista = new List<Ingrediente>();
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
            select *
            from Prato_possui_Ingrediente
            where prato=@id AND ingrediente LIKE @ing
            ";
            command.Parameters.Add(new SqlParameter("@id", idPrato));
            command.Parameters.Add(new SqlParameter("@ing", "%"+ing+"%"));
            var result = command.ExecuteReader();
            while (result.Read())
            {
                byte Customizavel = (byte)result["customizavel"];
                if (Customizavel == 0)
                {
                    r = false;
                    break;
                }
            }
            connection.Close();
            return r;
        }
        
    }
}