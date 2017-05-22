using PickaPratoServer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace PickaPratoServer.Data
{
    class ClienteDAO
    {

        SqlConnection connection = new SqlConnection("Server=DIOGO-PC\\SQLEXPRESS; Database=PickPrato; Trusted_Connection=True;");

        public Cliente GetCliente(String id)
        {

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                select *
                from Cliente
                where username = @username
            ";
            command.Parameters.Add(new SqlParameter("@username",id));
            var result = command.ExecuteReader();
            Cliente c = new Cliente();

            if (result.Read())
            {
                c.Username = (String)result["username"];
                c.Password = (String)result["password"];
                if (result["fotografia"] != System.DBNull.Value)
                    c.Foto = (String)result["fotografia"];
            }
            return c;
        }

        public void Put(Cliente c) {
            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                INSERT INTO Cliente
                    ([username],[password],[fotografia])
                VALUES (@username,@password,@foto)
            ";
            command.Parameters.Add(new SqlParameter("@username", c.Username));
            command.Parameters.Add(new SqlParameter("@password", c.Password));
            if(c.Foto==null) command.Parameters.Add(new SqlParameter("@foto", DBNull.Value));
            else command.Parameters.Add(new SqlParameter("@foto", c.Foto));
            //SqlParameter p = new SqlParameter("@foto", SqlDbType.VarBinary);
            //p.Value = c.foto;
            //Debug.Print(c.foto);
            //command.Parameters.AddWithValue("@foto", SqlDbType.Var).Value=c.foto;
            //command.Parameters.Add(new SqlParameter("@cidade", "NULL"));

            var result = command.ExecuteNonQuery();
        }
    }
}