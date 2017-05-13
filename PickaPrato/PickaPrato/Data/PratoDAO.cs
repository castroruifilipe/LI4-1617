using System;
using System.Data;
using System.Data.SqlClient;
using Android.Support.V7.App;
using Android.Util;

namespace PickaPrato.Data {

    public class PratoDAO {

        string connectionSQLServer = "Server=<ipAddress>;Database=<DBName>;User Id=<username>;Password=<password>;Trusted_Connection=true";
        
        public bool Contains(int idPrato) {
            try {
	            SqlConnection connection = new SqlConnection(connectionSQLServer);
	            connection.Open();

                SqlCommand command = connection.CreateCommand();
	            command.Connection = connection;
                command.CommandType = CommandType.Text;
				command.CommandText = @"
                    select *
                    from Prato
                    where
                        idPrato = @idPrato
                ";
                command.Parameters.Add(new SqlParameter("@idPrato",idPrato));

	            var result = command.ExecuteReader();

	            bool exists = result.HasRows;
                return exists;
            } catch (Exception exception) {
                Log.Debug("ConnectionSQLServer",exception.StackTrace);
                return false;
            }
        }
    }
}
