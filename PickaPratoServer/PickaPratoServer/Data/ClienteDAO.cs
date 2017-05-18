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


        public Cliente getCliente()
        {
            SqlConnection connection = new SqlConnection("Server=DIOGO-PC\\SQLEXPRESS; Database=PickPrato; Trusted_Connection=True;");

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
    }
}