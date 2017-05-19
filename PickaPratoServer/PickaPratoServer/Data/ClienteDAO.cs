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

        public Cliente GetCliente()
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
            command.Parameters.Add(new SqlParameter("@username","diogo"));
            var result = command.ExecuteReader();
            Cliente c = new Cliente();

            if (result.Read())
            {
                c.Username = (String)result["username"];
                c.Password = (String)result["password"];
            }
            return c;
        }

        public void Put(Cliente c)
        {

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = @"
                INSERT INTO Cliente
                    ([username],[password])
                VALUES (@username,@password)
            ";
            command.Parameters.Add(new SqlParameter("@username", c.Username));
            command.Parameters.Add(new SqlParameter("@password", c.Password));
            //command.Parameters.Add(new SqlParameter("@fotografia", "NULL"));
            //command.Parameters.Add(new SqlParameter("@cidade", "NULL"));

            var result = command.ExecuteNonQuery();
        }
    }
}