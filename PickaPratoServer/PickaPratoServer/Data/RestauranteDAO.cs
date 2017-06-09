using PickaPratoServer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PickaPratoServer.Data
{
    public class RestauranteDAO
    {
        SqlConnection connection = new SqlConnection("Server=DIOGO-PC\\SQLEXPRESS; Database=PickPrato; Trusted_Connection=True;");

        public Restaurante Get(String id)
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                select *
                from Restaurante
                where proprietario = @username
            ";
            command.Parameters.Add(new SqlParameter("@username", id));
            var result = command.ExecuteReader();
            Restaurante r = new Restaurante();

            if (result.Read())
            {
                r.Proprietario = (String)result["proprietario"];
                r.Password = (String)result["password"];
                r.Telefone = (String)result["telefone"];
                r.Localizacao = (String)result["localizacao"];
                r.Email = (String)result["email"];
                r.Estado = (Byte)result["estado"];
                r.Nome = (String)result["nome"];
            }
            result.Close();

            if (r.Estado == 0) {
                r.Fotografias = null;
                return r;
            }

            command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                select *
                from Fotografia
                where restaurante = @proprietario
            ";
            command.Parameters.Add(new SqlParameter("@proprietario", id));
            result = command.ExecuteReader();
            List<String> fotos = new List<string>();
            while (result.Read())
            {
                fotos.Add((String)result["fotografia"]);
            }
            r.Fotografias = fotos;

            connection.Close();
            return r;
        }

        public void Put(Restaurante r)
        {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                INSERT INTO [dbo].[Restaurante]
                    ([proprietario] ,[password] ,[localizacao]
                    ,[nome],[telefone] ,[email],[estado])
                VALUES (@proprietario,@password,@localizacao,@nome,@telefone,@email,@estado)
            ";
            command.Parameters.Add(new SqlParameter("@proprietario", r.Proprietario));
            command.Parameters.Add(new SqlParameter("@password", r.Password));
            command.Parameters.Add(new SqlParameter("@localizacao", r.Localizacao));
            command.Parameters.Add(new SqlParameter("@nome", r.Nome));
            command.Parameters.Add(new SqlParameter("@telefone", r.Telefone));
            command.Parameters.Add(new SqlParameter("@email", r.Email));
            command.Parameters.Add(new SqlParameter("@estado", r.Estado));

            var result = command.ExecuteNonQuery();

            if (r.Fotografias != null) {
                foreach (String s in r.Fotografias) {
                    command = connection.CreateCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"
                INSERT INTO [dbo].[Fotografia]
                    ([fotografia] ,[restaurante])
                VALUES (@fotografia,@restaurante)
                ";
                    command.Parameters.Add(new SqlParameter("@fotografia", s));
                    command.Parameters.Add(new SqlParameter("@restaurante", r.Proprietario));
                    result = command.ExecuteNonQuery();
                }
            }
        }
    }
}