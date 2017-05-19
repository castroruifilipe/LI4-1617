using System;
using System.IO;

using PickaPrato.Business;
using SQLite;


namespace PickaPrato.Data {

    public class UtilizadorDAO {

        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "db_sqlnet.db");
 

		private string insertUpdateData(Cliente data) {
			try {
				var db = new SQLiteConnection(path);
                if (db.Insert(data) != 0) {
                    db.Update(data);
                }
				return "Single data file inserted or updated";
			} catch (SQLiteException ex) {
				return ex.Message;
			}
		}

		private int findNumberRecords(string path) {
			try {
				var db = new SQLiteConnection(path);
				var count = db.ExecuteScalar<int>("SELECT Count(*) FROM Cliente");
				return count;
			}
			catch (SQLiteException) {
				return -1;
			}
		}
    }
}
