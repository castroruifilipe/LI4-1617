using System;
using System.IO;

using SQLite;

using PickaPrato.Business;


namespace PickaPrato.Data {
    
    public class Database {

        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "db_sqlnet.db");


		public string CreateDatabase() {
			try {
				var connection = new SQLiteConnection(path);
				connection.CreateTable<Pesquisa>();
                connection.CreateTable<PratoLite>();
				return "Database created";
            } catch (SQLiteException ex) {
                return ex.Message;
            }
        }
    }
}
